namespace API.Middleware
{
    /// <summary>
    /// The request timeout middleware
    /// </summary>
    public class RequestTimeoutMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<RequestTimeoutMiddleware> logger;

        /// <summary>Initializes a new instance of the <see cref="RequestTimeoutMiddleware" /> class.</summary>
        /// <param name="requestDelegate">The request delegate.</param>
        /// <param name="logger">The logger.</param>
        public RequestTimeoutMiddleware(RequestDelegate requestDelegate, ILogger<RequestTimeoutMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        /// <summary>Invokes the asynchronous.</summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            context.RequestAborted = cts.Token;

            try
            {
                await this.requestDelegate(context);
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                this.logger.LogWarning("Request timed out");
                context.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            }
        }
    }

}
