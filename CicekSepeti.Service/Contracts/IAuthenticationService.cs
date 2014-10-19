
using CicekSepeti.Model;
using Microsoft.Owin.Security;

namespace CicekSepeti.Service.Contracts
{
    public interface IAuthenticationService
    {
        void Login(UserDTO model, IAuthenticationManager authenticationManager );

        void Logout(IAuthenticationManager authenticationManager);
    }
}
