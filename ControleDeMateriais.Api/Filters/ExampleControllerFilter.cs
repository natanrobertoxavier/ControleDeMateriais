using ControleDeMateriais.Communication.Requests;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ControleDeMateriais.Api.Filters;

public class ExampleControllerFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        AddRegistrationExamples(schema, context);
        AddLoginExamples(schema, context);
        AddForgotPasswordExamples(schema, context);
        AddNewPasswordExamples(schema, context);
        AddRegisterMaterialExamples(schema, context);
    }

    private static void AddRegistrationExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestRegisterUserJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "name":
                    schema.Example = new OpenApiString("Conrado Xavier");
                    break;
                case "cpf":
                    schema.Example = new OpenApiString("000.000.000-00");
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
    private static void AddLoginExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestLoginJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "email":
                    schema.Example = new OpenApiString("exemple@gmail.com");
                    break;
                case "password":
                    schema.Example = new OpenApiString("123456");
                    break;
            }
        }
    }
    private static void AddForgotPasswordExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestForgotPasswordJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "email":
                    schema.Example = new OpenApiString("exemple@gmail.com");
                    break;
            }
        }
    }
    private static void AddNewPasswordExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestNewPasswordJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "email":
                    schema.Example = new OpenApiString("exemple@gmail.com");
                    break;
                case "recoveryCode":
                    schema.Example = new OpenApiString("AB12EF");
                    break;
                case "newPassword":
                    schema.Example = new OpenApiString("123456");
                    break;
                case "confirmPassword":
                    schema.Example = new OpenApiString("123456");
                    break;
            }
        }
    }

    private static void AddRegisterMaterialExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestRegisterMaterialJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "name":
                    schema.Example = new OpenApiString("Notebook I7");
                    break;
                case "description":
                    schema.Example = new OpenApiString("Notebook Dell Inspiron 15 Series 3000");
                    break;
                case "barCode":
                    schema.Example = new OpenApiString("7896094906020");
                    break;
                case "category":
                    schema.Example = new OpenApiInteger(1);
                    break;
            }
        }
    }
}
