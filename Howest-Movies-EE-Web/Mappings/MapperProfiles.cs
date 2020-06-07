using AutoMapper;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_Web.Controllers;
using Howest_Movies_EE_Web.ViewModels;
using System.Linq;

namespace WebAPI.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //Students
            CreateMap<StudentDetailDTO, StudentViewModel>()
                .ForMember(dto => dto.Cursussen,
                           opt => opt.MapFrom(s => s.Cursussen
                                                    .Select(c => c.Cursusnaam).ToArray()));
            CreateMap<StudentBasicDTO, StudentViewModel>();
            CreateMap<CreateStudentViewModel, StudentDTO>()
                .ForMember(dto => dto.Studnr,
                           opt => opt.MapFrom(s => s.Nr));
            CreateMap<StudentBasicDTO, UpdateStudentViewModel>();
            CreateMap<UpdateStudentViewModel, StudentDTO>()
                .ForMember(dto => dto.Studnr,
                           opt => opt.MapFrom(s => s.Nr));

            //Courses
            CreateMap<CursusDTO, CursusViewModel>();


        }
    }
}