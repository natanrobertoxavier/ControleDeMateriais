using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ControleDeMateriais.Api.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ControleDeMateriaisException)
        {
            ProcessControleDeMateriaisException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void ProcessControleDeMateriaisException(ExceptionContext context)
    {
        if (context.Exception is ExceptionValidationErrors)
        {
            ProcessErrorValidator(context);
        }
        else if (context.Exception is ExceptionLoginInvalid)
        {
            ProcessLoginInvalidException(context);
        }
        else if (context.Exception is ExceptionRecoveryErrors)
        {
            ProcessRecoveryErrors(context);
        }
        else if (context.Exception is ExceptionBorrowedMaterialErros)
        {
            ProcessBorrowedMaterialErrors(context);
        }
        else if (context.Exception is ExceptionNoContentErrors)
        {
            ProcessNoContentErrors(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void ProcessNoContentErrors(ExceptionContext context)
    {
        var errorValidator = context.Exception as ExceptionNoContentErrors;

        context.HttpContext.Response.StatusCode = 204;
        context.Result = new ObjectResult(new ResponseErrorJson(errorValidator.MessagesErrors));
    }

    private static void ProcessLoginInvalidException(ExceptionContext context)
    {
        var errorLogin = context.Exception as ExceptionLoginInvalid;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ResponseErrorJson(errorLogin.Message));
    }

    private static void ProcessErrorValidator(ExceptionContext context)
    {
        var errorValidator = context.Exception as ExceptionValidationErrors;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErrorJson(errorValidator.MessagesErrors));
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ErrorMessagesResource.ERRO_DESCONHECIDO));
    }

    private static void ProcessRecoveryErrors(ExceptionContext context)
    {
        var errorValidator = context.Exception as ExceptionRecoveryErrors;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
        context.Result = new ObjectResult(new ResponseErrorJson(errorValidator.Message));
    }

    private static void ProcessBorrowedMaterialErrors(ExceptionContext context)
    {
        var errorValidator = context.Exception as ExceptionBorrowedMaterialErros;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErrorJson(errorValidator.MessagesErrors));
    }
}
