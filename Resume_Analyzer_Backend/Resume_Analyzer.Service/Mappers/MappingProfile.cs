using AutoMapper;
using Resume_Analyzer.DataAccess.Models;
using Resume_Analyzer.Service.DTOs;

namespace Resume_Analyzer.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resume, ResumeDTO>().ReverseMap(); ;
            CreateMap<User, UserDTO>().ReverseMap(); ;
        }
    }
}
