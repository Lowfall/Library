using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Application.Configuration
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate ?? throw new Exception(nameof(requestDelegate));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (BadHttpRequestException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case BadHttpRequestException e:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = e.Message;
                    break;
                case Exception e:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = e.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status.Value;
            var jsonProblemDetails = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(jsonProblemDetails);
        }
    }
}