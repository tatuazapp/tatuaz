using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tatuaz.Shared.Domain.Dtos;

namespace Tatuaz.Gateway.Swagger;

public class MarkRequiredSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (type == null)
        {
            return;
        }

        if (type.GetCustomAttributes(typeof(NoUndefAttribute), inherit: true).Any())
        {
            if (schema.Required == null)
            {
                schema.Required = new HashSet<string>();
            }

            foreach (var property in schema.Properties.Keys)
            {
                schema.Required.Add(property);
            }
        }
    }
}
