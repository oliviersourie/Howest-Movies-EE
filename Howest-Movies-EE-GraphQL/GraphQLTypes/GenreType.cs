using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Genre;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class GenreType : ObjectGraphType<GenreDTO>
    {
        public GenreType()
        {
            Field(m => m.Id);
            Field(m => m.ImdbName);
            Field(m => m.Name);
        }
    }
}
