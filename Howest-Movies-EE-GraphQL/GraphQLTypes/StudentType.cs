using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using GraphQL.Types;
using System.Collections.Generic;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class StudentType : ObjectGraphType<StudentDetailDTO>
    {
        public StudentType()
        {
            Field(s => s.Nr);
            Field(s => s.Voornaam);
            Field(s => s.Familienaam).Description("Last name of a student");
            Field(s => s.Email);
            Field(s => s.Cursussen, type: typeof(ListGraphType<CursusType>));
            Field(s => s.AantalCursussen);
        }
    } 
}
