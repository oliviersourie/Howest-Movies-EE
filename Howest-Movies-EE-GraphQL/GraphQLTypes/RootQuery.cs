using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.Repositories;
using GraphQL.Types;
using System.Linq;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IStudentRepository studentRepository)
        {
            Field<ListGraphType<StudentType>>(
                "students",
                Description = "Get all students",
                resolve: context =>
                {
                    return studentRepository.GetAll();
                }
            );
            Field<ListGraphType<StudentType>>(
               "filter",
               arguments: new QueryArguments
               {
                  new  QueryArgument<IntGraphType> { Name = "maxcursussen"},
                  new  QueryArgument<StringGraphType> { Name = "naambevat"}
               },
               resolve: context =>
               {
                   string name = context.GetArgument<string>("naambevat");
                   int maxaantalcursussen = context.GetArgument<int>("maxcursussen");
                   return studentRepository.GetAll()
                                           .Where(s => s.Familienaam.ToLower().Contains(name.ToLower())
                                                    && s.AantalCursussen <= maxaantalcursussen);
               });
        }
    }

}
