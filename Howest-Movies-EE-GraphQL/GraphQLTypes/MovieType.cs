using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Movie;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class MovieType : ObjectGraphType<MovieDTO>
    {
        public MovieType()
        {
            Field(m => m.Id);
            Field(m => m.Title);
            Field(m => m.CoverUrl);
            Field(m => m.Year);
            Field(m => m.Rating);
            Field(m => m.Plot);
            Field(m => m.Kind);
            Field(m => m.Top250Rank);
            Field(m => m.OriginalAirDate);
            Field(m => m.Country);
        }
    }
}
