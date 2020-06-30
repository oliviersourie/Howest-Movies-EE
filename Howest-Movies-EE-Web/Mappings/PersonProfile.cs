using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_Web.ViewModels;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDTO, PersonViewModel>().ReverseMap();
            CreateMap<PersonDetailDTO, UpdatePersonViewModel>();
            CreateMap<PersonDetailDTO, PersonViewModel>().ReverseMap();

            CreateMap<CreatePersonViewModel, CreatePersonDTO>()
                .ForMember(vm => vm.Movies, 
                           dto => dto.MapFrom(g => g.Movies == null 
                                        ? Enumerable.Empty<MovieIdDTO>() 
                                        : CreateMovieIdEnumarable(g.Movies)
                           ));

            CreateMap<UpdatePersonViewModel, PersonDetailDTO>()
                .ForMember(dto => dto.MoviePeople, 
                           opt => opt.MapFrom(g => g.Movies == null 
                                       ? Enumerable.Empty<MovieIdDTO>() 
                                       : CreateMovieIdEnumarable(g.Movies)
                            ));

        }
        private IEnumerable<MovieIdDTO> CreateMovieIdEnumarable(IEnumerable<string> movies)
        {
            foreach (string id in movies)
            {
                yield return new MovieIdDTO()
                {
                    Id = Int16.Parse(id)
                };
            }
        }

    }
}
