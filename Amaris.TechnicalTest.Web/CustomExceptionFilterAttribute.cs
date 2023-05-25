using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using Serilog;
using System.Net;
using Amaris.TechnicalTest.Core.Exceptions;
using System;

namespace Amaris.TechnicalTest.Web
{
    [ExcludeFromCodeCoverage, AttributeUsage(AttributeTargets.All)]
    public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// 
        private readonly ILogger _log;


        public CustomExceptionFilterAttribute(ILogger log)
        {
            _log = log;
        }

        public override void OnException(ExceptionContext context)
        {
            _log.Error(context.Exception, context.Exception.Message);

            var msg = new
            {
                context.Exception.Message,
                ExceptionType = context.Exception.GetType().ToString()
            };

            var typeExceptionName = context.Exception.GetType().BaseType.Name;
            switch (typeExceptionName)
            {
                case nameof(DomainException):
                case nameof(TechnicalException):
                    {
                        Core.Dtos.ResponseGenericMessage response = CreateResponseMessage(msg.Message, msg.ExceptionType);
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Result = new ObjectResult(response);
                    }
                    break;
                case nameof(ObjectNotFoundException):
                    {
                        Core.Dtos.ResponseGenericMessage response = CreateResponseMessage(msg.Message, msg.ExceptionType);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Result = new ObjectResult(response);
                    }

                    break;
                default:
                    {
                        Core.Dtos.ResponseGenericMessage response = CreateResponseMessage(msg.Message, msg.ExceptionType);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Result = new ObjectResult(response);
                    }
                    break;
            }

        }

        private Core.Dtos.ResponseGenericMessage CreateResponseMessage(string message, string messageType)
        {
            return new Core.Dtos.ResponseGenericMessage
            {
                Message = message,
                MessageType = messageType
            };
        }
    }
}
