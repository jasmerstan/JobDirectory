﻿using JobDirectoryUtility;
using JobDirectoryWeb.Models;
using JobDirectoryWeb.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace JobDirectoryWeb.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClientFactory { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            responseModel = new APIResponse();
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("JobDirectoryAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                //Data will not be null in POST/PUT HTTP Calls
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorMessages = new List<string>()
                    {
                        Convert.ToString(ex.Message)
                    },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
