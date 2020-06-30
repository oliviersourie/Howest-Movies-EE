using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class PersonDetailType : PersonType
    {
        public PersonDetailType()
        {
            Field(p => p.Movies, type: typeof(ListGraphType<MovieType>));
        }
    }
}
