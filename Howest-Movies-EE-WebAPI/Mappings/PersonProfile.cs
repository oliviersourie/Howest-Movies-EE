using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_WebAPI.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Persons, UpdatePersonDTO>().ReverseMap();
            CreateMap<CreatePersonDTO, Persons>();
            CreateMap<Persons, PersonDetailDTO>()
                .ForMember(dto => dto.Role, 
                           opt => opt.MapFrom(mr => mr.MovieRole.Select(r => r.Role).FirstOrDefault()))
                .ForMember(dto => dto.MoviePeople, 
                           opt => opt.MapFrom(pr => pr.MovieRole.Select(p => new MovieIdDTO()
                                                                            {
                                                                                Id = p.Movie.Id
                                                                            })));
            CreateMap<Persons, PersonDTO>()
                .ForMember(dto => dto.Biography, 
                           opt => opt.MapFrom(p => p.Biography.TakeFirst(100)));
        }
    }
}
