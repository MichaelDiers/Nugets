namespace Md.GoogleCloudFunctions.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Google.Cloud.Functions.Framework;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    ///     Base for google cloud http functions.
    /// </summary>
    public abstract class HttpFunction<TCategoryName> : IHttpFunction
    {
        /// <summary>
        ///     An error logger.
        /// </summary>
        private readonly ILogger<TCategoryName> logger;

        /// <summary>
        ///     Creates a new instance of <see cref="HttpFunction{TCategoryName}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        protected HttpFunction(ILogger<TCategoryName> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Asynchronously handles the request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        public virtual async Task HandleAsync(HttpContext context)
        {
            try
            {
                await this.HandleMethodAsync(context);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "An unhandled error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        ///     Asynchronously handles the request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        ///     Sets the http status code to 404.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual Task HandleAnyAsync(HttpContext context, object? content)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Asynchronously handles the delete request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleDeleteAsync(HttpContext context, object? content)
        {
            await this.HandleAnyAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the get request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleGetAsync(HttpContext context, object? content)
        {
            await this.HandleAnyAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the head request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleHeadAsync(HttpContext context, object? content)
        {
            await this.HandleAnyAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the request method in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleMethodAsync(HttpContext context)
        {
            var content = await this.ReadContentAsync(context);

            if (string.Equals("GET", context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                await this.HandleGetAsync(context, content);
            }
            else if (string.Equals("POST", context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                await this.HandlePostAsync(context, content);
            }
            else if (string.Equals("PUT", context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                await this.HandlePutAsync(context, content);
            }
            else if (string.Equals("DELETE", context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                await this.HandleDeleteAsync(context, content);
            }
            else if (string.Equals("HEAD", context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                await this.HandleHeadAsync(context, content);
            }
        }

        /// <summary>
        ///     Asynchronously handles the post request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandlePostAsync(HttpContext context, object? content)
        {
            await this.HandleAnyAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the put request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandlePutAsync(HttpContext context, object? content)
        {
            await this.HandleAnyAsync(context, content);
        }

        /// <summary>
        ///     Read the application/json content of the request.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A <see cref="Task" /> whose result is the request content.</returns>
        protected virtual async Task<object?> ReadContentApplicationJsonAsync(HttpContext context)
        {
            return await this.ReadContentTextPlainAsync(context);
        }

        /// <summary>
        ///     Read the application/x-www-form-urlencoded content of the request.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A <see cref="Task" /> whose result is the request content.</returns>
        protected virtual Task<object?> ReadContentApplicationXWwwFormUrlencodedAsync(HttpContext context)
        {
            return Task.FromResult<object?>(
                new ReadOnlyDictionary<string, string>(
                    new Dictionary<string, string>(
                        context.Request.Form.Select(item => new KeyValuePair<string, string>(item.Key, item.Value)))));
        }

        /// <summary>
        ///     Read the content of the request.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A <see cref="Task" /> whose result is the request content.</returns>
        protected virtual async Task<object?> ReadContentAsync(HttpContext context)
        {
            if (string.Equals(
                    "application/json",
                    context.Request.ContentType,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return await this.ReadContentApplicationJsonAsync(context);
            }

            if (string.Equals("text/plain", context.Request.ContentType, StringComparison.InvariantCultureIgnoreCase))
            {
                return await this.ReadContentTextPlainAsync(context);
            }

            if (string.Equals(
                    "application/x-www-form-urlencoded",
                    context.Request.ContentType,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return await this.ReadContentApplicationXWwwFormUrlencodedAsync(context);
            }

            return null;
        }

        /// <summary>
        ///     Read the text/plain content of the request.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <returns>A <see cref="Task" /> whose result is the request content.</returns>
        protected virtual async Task<object?> ReadContentTextPlainAsync(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        ///     Sets the response content to application/json.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="statusCode">The response http status code.</param>
        /// <param name="content">The <see cref="object" /> is serialized and set to the response.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected virtual async Task SetJsonResponse(HttpContext context, HttpStatusCode statusCode, object content)
        {
            var json = JsonConvert.SerializeObject(content);
            await this.SetJsonResponse(context, statusCode, json);
        }

        /// <summary>
        ///     Sets the response content to application/json.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="statusCode">The response http status code.</param>
        /// <param name="json">The json is set to the response.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected virtual async Task SetJsonResponse(HttpContext context, HttpStatusCode statusCode, string json)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        /// <summary>
        ///     Sets the response content to text/plain.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="statusCode">The response http status code.</param>
        /// <param name="text">The text is set to the response.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected virtual async Task SetTextPlainResponse(HttpContext context, HttpStatusCode statusCode, string text)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(text);
        }
    }
}
