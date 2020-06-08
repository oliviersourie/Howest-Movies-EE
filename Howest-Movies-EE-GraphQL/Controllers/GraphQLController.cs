using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.GraphQLDTO;
using Microsoft.AspNetCore.Mvc;

namespace Howest_Movies_EE_GraphQL.Controllers
{
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;

        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            this.schema = schema;
            this.executer = executer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]QraphQLQueryDTO query)
        {
            ExecutionResult result = await executer.ExecuteAsync(r =>
            {
                r.Schema = schema;
                r.Query = query.Query;
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return Problem(detail: result.Errors.Select(e => e.Message).FirstOrDefault(), statusCode: 500);
            }
            return Ok(result.Data);
        }
    }
}