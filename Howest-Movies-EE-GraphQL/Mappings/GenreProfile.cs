using AutoMapper;
using Howest_Movies_EE_DAL.DTO.Genre;
using Howest_Movies_EE_DAL.DTO.Movie;
using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.Extensions;
using System.Linq;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genres, GenreDTO>()
              .ForMember(dto => dto.Movies,
                         opt => opt.MapFrom(m => m.GenreMovie
                                                  .Select(gm => new MovieDTO
                                                  {
                                                      Id = gm.Movie.Id,
                                                      CoverUrl = gm.Movie.CoverUrl,
                                                      ImdbId = gm.Movie.ImdbId,
                                                      Kind = gm.Movie.Kind,
                                                      OriginalAirDate = gm.Movie.OriginalAirDate,
                                                      Plot = gm.Movie.Plot.RemoveChars("[]").RemoveDoubleQuotes(),
                                                      Rating = gm.Movie.Rating,
                                                      Title = gm.Movie.Title,
                                                      Top250Rank = gm.Movie.Top250Rank,
                                                      Year = gm.Movie.Year
                                                  }).ToList()));
        }
    }
}