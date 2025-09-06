namespace InfoTrack.Api.Core
{
    public class ConcurrencyRequestLimitter
    {
        private readonly RequestDelegate _next;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(4, 4);

        public ConcurrencyRequestLimitter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!await _semaphore.WaitAsync(0))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("InfoTrack Api won't be able to process more than 4 request simultaneously.");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
