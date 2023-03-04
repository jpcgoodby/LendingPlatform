using MediatR;
using Microsoft.Extensions.Logging;

namespace LendingPlatform.Service.Behaviours
{
    namespace LendingPlatform.Service.Behaviours
    {
        public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        {
            private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

            public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) => _logger = logger;

            public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Lending platform has started. Processing application with {typeof(TRequest).Name}");

                var response = await next();

                _logger.LogInformation($"Lending platform has completed.  Processed with {typeof(TResponse).Name}");

                return response;
            }
        }
    }
}
