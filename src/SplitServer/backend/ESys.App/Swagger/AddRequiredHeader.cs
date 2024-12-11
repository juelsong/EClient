using ESys.Utilty.Defs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ESys.Swagger
{
    public class AddRequiredHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = ConstDefs.RequestHeader.Tenant,
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema() { Type = "", Default = new OpenApiString("Emis") }
            }); ;
        }
    }
}