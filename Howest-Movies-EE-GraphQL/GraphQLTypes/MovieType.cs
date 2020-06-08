using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.MovieDTO;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class MovieType : ObjectGraphType<ListItemMovieDTO>
    {
        public MovieType()
        {
            Field(m => m.Id);
            Field(m => m.Title);
            Field(m => m.CoverUrl);
            Field(m => m.Year);
            Field(m => m.Rating);
            Field(m => m.OriginalAirDate);
            Field(m => m.Country);
        }
    }
}
