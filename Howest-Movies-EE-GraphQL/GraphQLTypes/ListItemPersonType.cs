using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.PersonDTO;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class ListItemPersonType : ObjectGraphType<ListItemPersonDTO>
    {
        public ListItemPersonType()
        {
            Field(m => m.Id);
            Field(m => m.Name);
            Field(m => m.Biography);
        }
    }
}
