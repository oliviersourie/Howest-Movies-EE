using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.PersonDTO;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class PersonType : ObjectGraphType<FullPersonDTO>
    {

        public PersonType()
        {
            Field(m => m.Id);
            Field(m => m.ImdbId);
            Field(m => m.Name);
            Field(m => m.Role);
            Field(m => m.Biography);
        }
    }
}
