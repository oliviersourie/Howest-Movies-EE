using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.Genre;
using Howest_Movies_EE_DAL.DTO.Movie;
using Howest_Movies_EE_DAL.DTO.Person;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movies, MovieDetailDTO>()
                .ForMember(dto => dto.Plot, 
                           opt => opt.MapFrom(m => m.Plot.RemoveChars("[]").RemoveDoubleQuotes()))

                .ForMember(dto => dto.Genres, 
                           opt => opt.MapFrom(mg => mg.GenreMovie
                                                      .Select(g => new GenreDTO
                                                                {
                                                                    Id = g.Genre.Id,
                                                                    ImdbName = g.Genre.ImdbName,
                                                                    Name = g.Genre.Name
                                                                })
                                                      .ToList()))
                .ForMember(dto => dto.Persons, 
                           opt => opt.MapFrom(mr => mr.MovieRole/*.Where(p => p.Role.Equals("actor"))*/
                                                      .Select(a => new PersonDTO
                                                                {
                                                                    Id = a.Person.Id,
                                                                    ImdbId = a.Person.ImdbId,
                                                                    Name = a.Person.Name,
                                                                    Biography = a.Person.Biography,
                                                                    Role = a.Role,
                                                                })
                                                      .ToList()));
        }
    }
}
