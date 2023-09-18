using JobDirectoryWeb.Models.DTO;
using System.Linq.Expressions;

namespace JobDirectoryWeb.Services.IServices
{
    public interface IJobDirectoryService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(JobDirectoryCreateDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(JobDirectoryUpdateDTO dto);
    }
}
