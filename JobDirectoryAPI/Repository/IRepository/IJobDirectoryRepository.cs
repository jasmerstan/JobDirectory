using JobDirectoryAPI.Models;

namespace JobDirectoryAPI.Repository.IRepository
{
    public interface IJobDirectoryRepository : IRepository<JobDirectory>
    {
        Task<JobDirectory> UpdateAsync(JobDirectory entity);
    }
}
