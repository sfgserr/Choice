using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace WebApi.Seed
{
    public class SeedClients
    {
        private readonly IOpenIddictApplicationManager _appManager;
        private readonly ClientsOption _clientsOption;

        public SeedClients(IOpenIddictApplicationManager appManager, IOptions<ClientsOption> clientsOption)
        {
            _appManager = appManager;
            _clientsOption = clientsOption.Value;
        }

        public async Task Seed()
        {
            foreach (var c in _clientsOption.Clients)
            {
                if (await _appManager.FindByClientIdAsync(c.ClientId) is null)
                {
                    await _appManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = c.ClientId,
                        ClientSecret = c.ClientSecret,
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.Password + "_phone",
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                        }
                    });
                }
            }
        }
    }
}