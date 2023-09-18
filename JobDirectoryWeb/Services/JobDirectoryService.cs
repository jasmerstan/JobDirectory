using JobDirectoryUtility;
using JobDirectoryWeb.Models;
using JobDirectoryWeb.Models.DTO;
using JobDirectoryWeb.Services.IServices;

namespace JobDirectoryWeb.Services
{
    public class JobDirectoryService : BaseService, IJobDirectoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string jobDirectoryUrl;
        public JobDirectoryService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            jobDirectoryUrl = configuration.GetValue<string>("ServiceUrls:JobDirectoryAPI");
        }
        public Task<T> CreateAsync<T>(JobDirectoryCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = jobDirectoryUrl + "/api/jobDirectoryAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = jobDirectoryUrl + "/api/jobDirectoryAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = jobDirectoryUrl + "/api/jobDirectoryAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = jobDirectoryUrl + "/api/jobDirectoryAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(JobDirectoryUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = jobDirectoryUrl + "/api/jobDirectoryAPI/"+ dto.Id,
            });
        }
    }
}
