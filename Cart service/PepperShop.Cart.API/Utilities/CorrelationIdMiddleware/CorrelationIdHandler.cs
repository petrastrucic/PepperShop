namespace PepperShop.Cart.API.Utilities.CorrelationIdMiddleware
{
    public class CorrelationIdHandler : DelegatingHandler
    {
        private const string CorrelationHeader = "X-Correlation-ID";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = CorrelationContext.CorrelationId ?? Guid.NewGuid().ToString();

            if (!request.Headers.Contains(CorrelationHeader))
            {
                request.Headers.Add(CorrelationHeader, correlationId);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
