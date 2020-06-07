using GraphQL;
using GraphQL.Types;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
        }
    }
}
