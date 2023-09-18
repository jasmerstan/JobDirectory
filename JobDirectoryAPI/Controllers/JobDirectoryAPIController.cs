using AutoMapper;
using JobDirectoryAPI.Models;
using JobDirectoryAPI.Models.DTO;
using JobDirectoryAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDirectoryAPIController : ControllerBase
    {
        private readonly IJobDirectoryRepository _dbJobDir;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public JobDirectoryAPIController(IJobDirectoryRepository dbJobDir, IMapper mapper)
        {
            _dbJobDir = dbJobDir;
            _mapper = mapper;
            _response = new APIResponse();
        }
        //Endpoint
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<APIResponse>> GetJobDirectories()
        {
            try
            {
                IEnumerable<JobDirectory> jobDirList = await _dbJobDir.GetAllAsync();
                _response.Result = _mapper.Map<List<JobDirectoryDTO>>(jobDirList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        [HttpGet("{id:int}", Name = "GetJobDirectory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetJobDirectory(int id, string name)
        {
            try
            {
                //if (id == 0)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}
                var jobDir = await _dbJobDir.GetAsync(x => x.UserName == name);
                if (jobDir == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<JobDirectoryDTO>(jobDir);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] JobDirectoryDTO createDTO)
        {
            try
            {
                if (await _dbJobDir.GetAsync(u => u.UserName.ToLower() == createDTO.UserName.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Job directory already exist.");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }
                JobDirectory jobDir = _mapper.Map<JobDirectory>(createDTO);
                await _dbJobDir.CreateAsync(jobDir);
                _response.Result = _mapper.Map<JobDirectoryDTO>(jobDir);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetJobDirectory", new { id = jobDir.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteJobDirectory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> DeleteVilla(int id)
        public async Task<ActionResult<APIResponse>> DeleteJobDirectory(int id, string name)
        {
            try
            {
                //if (id == 0)
                //{
                //    return BadRequest();
                //}
                var jobDir = await _dbJobDir.GetAsync(x => x.UserName == name);
                if (jobDir == null)
                {
                    return NotFound();
                }
                await _dbJobDir.RemoveAsync(jobDir);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        [HttpPut("{id:int}", Name = "UpdateJobDirectory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateJobDirectory(int id, string name, [FromBody] JobDirectoryDTO jobDirectoryDTO)
        {
            try
            {
                //if (jobDirectoryDTO == null || id != jobDirectoryDTO.Id)

                if (jobDirectoryDTO == null)
                {
                    return BadRequest();
                }
                JobDirectory jobDir = _mapper.Map<JobDirectory>(jobDirectoryDTO);
                await _dbJobDir.UpdateAsync(jobDir);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, string name, JsonPatchDocument<JobDirectoryDTO> patchDTO)
        {
            //if (patchDTO == null || id == 0)
            if (patchDTO == null)
            {
                return BadRequest();
            }
            var jobDir = await _dbJobDir.GetAsync(x => x.UserName == name, tracked: false);
            if (jobDir == null)
            {
                return NotFound();
            }
            JobDirectoryDTO jobDirDTO = _mapper.Map<JobDirectoryDTO>(jobDir);
            patchDTO.ApplyTo(jobDirDTO, ModelState);
            JobDirectory model = _mapper.Map<JobDirectory>(jobDirDTO);
            await _dbJobDir.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
