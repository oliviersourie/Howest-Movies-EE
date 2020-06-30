using AutoMapper;
using Howest_Movies_EE_DAL.DTO.Movie;
using Howest_Movies_EE_DAL.DTO.Person;
using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.Extensions;
using System.Linq;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Persons, PersonDTO>()
                .ForMember(dto => dto.Movies,
                         opt => opt.MapFrom(m => m.MovieRole
                                                  .Select(mr => new MovieDTO
                                                  {
                                                      Id = mr.Movie.Id,
                                                      CoverUrl = mr.Movie.CoverUrl,
                                                      ImdbId = mr.Movie.ImdbId,
                                                      Kind = mr.Movie.Kind,
                                                      OriginalAirDate = mr.Movie.OriginalAirDate,
                                                      Plot = mr.Movie.Plot.RemoveChars("[]").RemoveDoubleQuotes(),
                                                      Rating = mr.Movie.Rating,
                                                      Title = mr.Movie.Title,
                                                      Top250Rank = mr.Movie.Top250Rank,
                                                      Year = mr.Movie.Year
                                                  }).ToList()));
        }
    }
}
