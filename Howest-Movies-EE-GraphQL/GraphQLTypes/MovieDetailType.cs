using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Movie;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class MovieDetailType : ObjectGraphType<MovieDetailDTO>
    {
        public MovieDetailType()
        {
            Field(m => m.Id);
            Field(m => m.Title);
            Field(m => m.Year);
            Field(m => m.OriginalAirDate);
            Field(m => m.Rating);
            Field(m => m.Plot);
            Field(m => m.Top250Rank);
            Field(m => m.CoverUrl);
            Field(m => m.Kind);
            Field(m => m.Country);
            Field(m => m.Genres, type: typeof(ListGraphType<GenreType>));
            Field(m => m.Persons, type: typeof(ListGraphType<PersonType>));
        }
    }
}
