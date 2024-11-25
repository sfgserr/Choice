using OpenIddict.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Configuration.Authentication.GrantTypeHandling
{
    public interface IGrantTypeHandler
    {
        string GrantType { get; }
        
        Task<IActionResult> Handle(OpenIddictRequest request, Controller controller);
    }
}