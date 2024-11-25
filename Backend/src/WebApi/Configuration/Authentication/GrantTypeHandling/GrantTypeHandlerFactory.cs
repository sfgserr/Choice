using OpenIddict.Abstractions;

namespace WebApi.Configuration.Authentication.GrantTypeHandling
{
    public class GrantTypeHandlerFactory
    {
        private readonly IEnumerable<IGrantTypeHandler> _handlers;

        public GrantTypeHandlerFactory(IEnumerable<IGrantTypeHandler> handlers)
        {
            _handlers = handlers;
        }

        public IGrantTypeHandler GetHandler(OpenIddictRequest request)
        {
            return _handlers.FirstOrDefault(h => h.GrantType == request.GrantType) ?? 
                   throw new ArgumentException("Such grant type is not implemented");
        }
    }
}