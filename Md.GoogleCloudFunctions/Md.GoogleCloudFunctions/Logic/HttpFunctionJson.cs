namespace Md.GoogleCloudFunctions.Logic
{
    using System.Threading.Tasks;
    using Md.Common.Logic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public abstract class HttpFunctionJson<TCategoryName, TContent> : HttpFunction<TCategoryName> where TContent : class
    {
        /// <summary>
        ///     Creates a new instance of <see cref="HttpFunctionJson{TCategoryName, TContent}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        protected HttpFunctionJson(ILogger<TCategoryName> logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Asynchronously handles the request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandleAnyAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandleAnyContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        ///     Sets the http status code to 404.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual Task HandleAnyContentAsync(HttpContext context, TContent? content)
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
        protected override async Task HandleDeleteAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandleDeleteContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the delete request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleDeleteContentAsync(HttpContext context, TContent? content)
        {
            await this.HandleAnyContentAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the get request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandleGetAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandleGetContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the get request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleGetContentAsync(HttpContext context, TContent? content)
        {
            await this.HandleAnyContentAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the head request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandleHeadAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandleHeadContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the head request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandleHeadContentAsync(HttpContext context, TContent? content)
        {
            await this.HandleAnyContentAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the post request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandlePostAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandlePostContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the post request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandlePostContentAsync(HttpContext context, TContent? content)
        {
            await this.HandleAnyContentAsync(context, content);
        }

        /// <summary>
        ///     Asynchronously handles the put request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandlePutAsync(HttpContext context, object? content)
        {
            var obj = content is string json ? Serializer.DeserializeObject<TContent>(json) : null;
            await this.HandlePutContentAsync(context, obj);
        }

        /// <summary>
        ///     Asynchronously handles the put request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected virtual async Task HandlePutContentAsync(HttpContext context, TContent? content)
        {
            await this.HandleAnyContentAsync(context, content);
        }
    }
}
