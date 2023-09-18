using AutoMapper;
using JobDirectoryWeb.Models;
using JobDirectoryWeb.Models.DTO;

namespace JobDirectoryWeb
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<JobDirectoryDTO, JobDirectoryCreateDTO>().ReverseMap();
            CreateMap<JobDirectoryDTO, JobDirectoryUpdateDTO>().ReverseMap();
        }
    }
}
