using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movies, ListItemMovieDTO>();
            CreateMap<Movies, SmallMovieDTO>();
            CreateMap<Movies, MovieDetailDTO>()

                .ForMember(dto => dto.Plot, opt => opt.MapFrom(
                    m => m.Plot.RemoveChars("[]").RemoveDoubleQuotes()
                    ))

                .ForMember(dto => dto.GenreMovie, opt => opt.MapFrom(
                    sg => sg.GenreMovie
                        .Select(
                            g => new FullGenreDTO
                            {
                                Id = g.Genre.Id,
                                ImdbName = g.Genre.ImdbName,
                                Name = g.Genre.Name
                            }

                        ).ToList()
                ))
            .ForMember(dto => dto.Actors, opt => opt.MapFrom(
                mr => mr.MovieRole

                .Where(p => p.Role.Equals("actor"))

                .Select(
                        a => new FullPersonDTO
                        {

                            Id = a.Person.Id,
                            ImdbId = a.Person.ImdbId,
                            Name = a.Person.Name,
                            Biography = a.Person.Biography,
                            Role = a.Role,
                        }
                    )
             ));
        }
    }
}
