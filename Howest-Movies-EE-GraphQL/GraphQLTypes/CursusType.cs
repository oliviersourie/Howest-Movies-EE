using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using GraphQL.Types;
using System.Collections.Generic;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class CursusType : ObjectGraphType<CursusDTO>
    {
        public CursusType()
        {
            Name = "Cursus";

            Field(c => c.Cursusnr);
            Field(c => c.Cursusnaam);
        }
    }
  
}
