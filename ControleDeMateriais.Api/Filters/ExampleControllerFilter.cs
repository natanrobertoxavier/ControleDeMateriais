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
        AddRegisterUserExamples(schema, context);
        AddLoginExamples(schema, context);
        AddForgotPasswordExamples(schema, context);
        AddNewPasswordExamples(schema, context);
        AddRegisterMaterialExamples(schema, context);
        AddUpdateMaterialExamples(schema, context);
        AddRegisterCollaboratorExamples(schema, context);
        AddUpdateCollaboratorExamples(schema, context);
        AddRegisterMaterialsLoanExamples(schema, context);
        AddConfirmSelectedMaterialExamples(schema, context);
        AddMaterialDevolutionExamples(schema, context);
    }

    private static void AddRegisterUserExamples(OpenApiSchema schema, SchemaFilterContext context)
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
    private static void AddUpdateMaterialExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestUpdateMaterialJson))
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
    private static void AddRegisterCollaboratorExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestCollaboratorJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "name":
                    schema.Example = new OpenApiString("Antônio Francisco Pereira Xavier");
                    break;
                case "nickname":
                    schema.Example = new OpenApiString("Antônio Xavier");
                    break;
                case "enrollment":
                    schema.Example = new OpenApiString("000.000-0");
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
    private static void AddUpdateCollaboratorExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestUpdateCollaboratorJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "name":
                    schema.Example = new OpenApiString("Antônio Francisco Pereira Xavier");
                    break;
                case "nickname":
                    schema.Example = new OpenApiString("Antônio Xavier");
                    break;
                case "email":
                    schema.Example = new OpenApiString("exemple@gmail.com");
                    break;
                case "telephone":
                    schema.Example = new OpenApiString("XX 9 XXXX-XXXX");
                    break;
            }
        }
    }
    private static void AddRegisterMaterialsLoanExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestMaterialSelectionJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "collaboratorId":
                    schema.Example = new OpenApiString("65847d709d253373dbf96bcb");
                    break;
                case "barCode":
                    schema.Example = new OpenApiArray
                    {
                        new OpenApiString("7896094906019"),
                        new OpenApiString("7896094906020"),
                        new OpenApiString("7896094906021")
                    };
                    break;
            }
        }
    }
    private static void AddConfirmSelectedMaterialExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestConfirmSelectedMaterialJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "hashId":
                    schema.Example = new OpenApiString("9f7a961a2a89b9a6c23d06de55a7bddc288c2aee0e922e39bc18bcd733da3068");
                    break;
                case "enrollment":
                    schema.Example = new OpenApiString("148.914-3");
                    break;
                case "password":
                    schema.Example = new OpenApiString("123456");
                    break;
            }
        }
    }
    private static void AddMaterialDevolutionExamples(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is PropertyInfo propertyInfo && context.MemberInfo.DeclaringType == typeof(RequestMaterialDevolutionJson))
        {
            var propertyName = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..];
            switch (propertyName)
            {
                case "hashId":
                    schema.Example = new OpenApiString("9f7a961a2a89b9a6c23d06de55a7bddc288c2aee0e922e39bc18bcd733da3068");
                    break;
                case "barCode":
                    schema.Example = new OpenApiArray
                    {
                        new OpenApiString("7896094906019"),
                        new OpenApiString("7896094906020"),
                        new OpenApiString("7896094906021")
                    };
                    break;
            }
        }
    }
}
