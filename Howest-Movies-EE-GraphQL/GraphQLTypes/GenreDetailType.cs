using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Genre;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class GenreDetailType : GenreType
    {
        public GenreDetailType()
        {
            Field(m => m.Movies, type: typeof(ListGraphType<MovieType>));
        }
    }
}
