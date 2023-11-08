﻿using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Exceptions;
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
}