using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<CreateGenreViewModel, CreateGenreDTO>()
                .ForMember(dto => dto.Movies, 
                           opt => opt.MapFrom(g => g.Movies == null 
                                                   ? Enumerable.Empty<MovieIdDTO>() 
                                                   : CreateMovieIdEnumarable(g.Movies)
                            )) ;
            CreateMap<UpdateGenreViewModel, UpdateGenreDTO>()
                .ForMember(dto => dto.Movies, 
                           opt => opt.MapFrom(g => g.Movies == null 
                                                    ? Enumerable.Empty<MovieIdDTO>() 
                                                    : CreateMovieIdEnumarable(g.Movies)
                            ));
            CreateMap<UpdateGenreViewModel, FullGenreDTO>().ReverseMap();
        }

        private IEnumerable<MovieIdDTO> CreateMovieIdEnumarable(IEnumerable<string> movies)
        {
            foreach(string id in movies)
            {
                yield return new MovieIdDTO()
                {
                    Id = Int16.Parse(id)
                };
            }            
        }
    }
}
