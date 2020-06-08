using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_WebAPI.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genres, FullGenreDTO>()
                .ForMember(dto => dto.MovieGenres, 
                           opt => opt.MapFrom(sg => sg.GenreMovie.Select(g => new MovieIdDTO
                                                                                {
                                                                                    Id = g.MovieId
                                                                                })
                                                                 .ToList()));
            CreateMap<UpdateGenreDTO, Genres>().ReverseMap();
            CreateMap<SmallGenreDTO, Genres>().ReverseMap();
            CreateMap<CreateGenreDTO, Genres>().ReverseMap();
            CreateMap<SmallGenreDTO, FullGenreDTO>();

        }

    }
}
