using AutoMapper;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using System.Linq;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Studenten, StudentDTO>().ReverseMap();

            CreateMap<Studenten, StudentBasicDTO>()
                .ForMember(dto => dto.Nr,
                           opt => opt.MapFrom(s => s.Studnr));

            CreateMap<Studenten, StudentDetailDTO>()
                .ForMember(dto => dto.Nr,
                           opt => opt.MapFrom(s => s.Studnr))
                .ForMember(dto => dto.Cursussen,
                           opt => opt.MapFrom(s => s.StudentenCursussen
                                                    .Select(sc => new CursusDTO
                                                    {
                                                        Cursusnr = sc.CursusnrNavigation.Cursusnr,
                                                        Cursusnaam = sc.CursusnrNavigation.Cursusnaam,
                                                        Inschrijvingsgeld = sc.CursusnrNavigation.Inschrijvingsgeld
                                                    }).ToList()))
                .ForMember(dto => dto.AantalCursussen,
                           opt => opt.MapFrom(s => s.StudentenCursussen.Count));
            CreateMap<Cursussen, CursusDTO>();
        }
    }
}