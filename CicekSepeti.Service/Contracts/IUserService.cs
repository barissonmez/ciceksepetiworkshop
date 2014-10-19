
using CicekSepeti.Model;
using Microsoft.Owin.Security;

namespace CicekSepeti.Service.Contracts
{
    public interface IUserService
    {
        UserDTO GetUser(string email, string hashedPassword);
        ResultModel Register(NewUserViewModel model);
        ResultModel Login(LoginViewModel model);
        bool IsEmailExist(string email);
    }
}
