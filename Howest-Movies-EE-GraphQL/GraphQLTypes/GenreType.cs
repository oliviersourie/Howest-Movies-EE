using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.DTO.GenreDTO;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class GenreType : ObjectGraphType<FullGenreDTO>
    {
        public GenreType()
        {
            Field(m => m.Id);
            Field(m => m.ImdbName);
            Field(m => m.Name);
        }
    }
}
