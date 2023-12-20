using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        User? GetAuthenticatedUser(string username, string password);

    }
}
