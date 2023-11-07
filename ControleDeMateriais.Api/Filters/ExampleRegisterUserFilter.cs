using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ControleDeMateriais.Api.Filters;

public class ExampleRegisterUserFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestRegisterUserJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1);
            switch (propertyName)
            {
                case "name":
                    schema.Example = new OpenApiString("Conrado Xavier");
                    break;
                case "email":
                    schema.Example = new OpenApiString("exemple@gmail.com");
                    break;
                case "password":
                    schema.Example = new OpenApiString("123456");
                    break;
                case "telephone":
                    schema.Example = new OpenApiString("XX 9 XXXX-XXXX");
                    break;
            }
        }
    }
}
