using AutoMapper;
using Backend.Business_Logic_Layer.DTO;
using Backend.Data_Access_Layer.Models;

namespace Backend.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Map from Student entity to DTO and vice versa
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, CreateStudentDTO>().ReverseMap();
        }
    }
}
