using AutoMapper;
using JobDirectoryWeb.Models;
using JobDirectoryWeb.Models.DTO;
using JobDirectoryWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace JobDirectoryWeb.Controllers
{
    public class JobDirectoryController : Controller
    {
        private readonly IJobDirectoryService _jobDirectoryService;
        private readonly IMapper _mapper;

        public JobDirectoryController(IJobDirectoryService jobDirectoryService, IMapper mapper)
        {
            _jobDirectoryService = jobDirectoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<JobDirectoryDTO> list = new();
            var response = await _jobDirectoryService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<JobDirectoryDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateJobDirectory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateJobDirectory(JobDirectoryCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _jobDirectoryService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        public async Task<IActionResult> UpdateJobDirectory(int id)
        {
            var response = await _jobDirectoryService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                JobDirectoryDTO model = JsonConvert.DeserializeObject<JobDirectoryDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<JobDirectoryUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateJobDirectory(JobDirectoryUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _jobDirectoryService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteJobDirectory(int id)
        {
            var response = await _jobDirectoryService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                JobDirectoryDTO model = JsonConvert.DeserializeObject<JobDirectoryDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteJobDirectory(JobDirectoryDTO model)
        {
            var response = await _jobDirectoryService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
