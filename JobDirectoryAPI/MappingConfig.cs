using AutoMapper;
using JobDirectoryAPI.Models;
using JobDirectoryAPI.Models.DTO;

namespace JobDirectoryAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<JobDirectory, JobDirectoryDTO>().ReverseMap();
            CreateMap<JobDirectory, JobDirectoryCreateDTO>().ReverseMap();
        }
    }
}
