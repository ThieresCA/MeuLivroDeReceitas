using MeuLivroDeReceitas.Comunication.Response;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeuLivroDeReceitas.Api.Filters
{
    //esse filtro será registrado no program, para que cada vez q acontecer um erro, essa classe seja chamada
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MeuLivroDeReceitasException)
            {
                MeuLivroDeReceitasExceptionTreatment(context);
            }
            else if (context.Exception is InvalidLogInException)
            {
                LogInExcepctionTreatment(context);
            }
            else
            {
                UnknownError(context);
            };
        }

        private void MeuLivroDeReceitasExceptionTreatment(ExceptionContext context)
        {
            if (context.Exception is ValidationErrorsExceptions)
            {
                ErrorsValidationTreatment(context);
            }
        }

        private void LogInExcepctionTreatment(ExceptionContext context)
        {
            var validationError = context.Exception as ValidationErrorsExceptions;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new ObjectResult(new ErrorResponseJson(validationError.Message));
        }

        private void ErrorsValidationTreatment(ExceptionContext context)
        {
            var ErrorsValidationException = context.Exception as ValidationErrorsExceptions;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ErrorResponseJson(ErrorsValidationException.ErrorsMessage));
        }

        private void UnknownError(ExceptionContext context)
        {
            //setando o status code 500 para o erro
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorResponseJson(ResourceErrorMessage.UNKNOWN_ERROR));
        }
    }
}
