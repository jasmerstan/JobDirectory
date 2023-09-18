using JobDirectoryAPI.Data;
using JobDirectoryAPI.Models;
using JobDirectoryAPI.Repository.IRepository;

namespace JobDirectoryAPI.Repository
{
    public class JobDirectoryRepository : Repository<JobDirectory>, IJobDirectoryRepository
    {
        private readonly ApplicationDbContext _db;
        public JobDirectoryRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public async  Task<JobDirectory> UpdateAsync(JobDirectory entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.JobDirectories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
