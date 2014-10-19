using System;
using System.Security.Claims;
using CicekSepeti.Entity;
using CicekSepeti.Facility;
using CicekSepeti.Model;
using CicekSepeti.Repository.Contracts;
using CicekSepeti.Repository.Implementations;
using CicekSepeti.Service.Contracts;
using FastMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CicekSepeti.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserRepository _userRepo;
        private readonly IUnitOfWork _uow;

        public AuthenticationService(UserRepository userRepo, IUnitOfWork uow)
        {
            _userRepo = userRepo;
            _uow = uow;
        }


        public void Login(UserDTO model, IAuthenticationManager authenticationManager)
        {
            if (authenticationManager.User.Identity.IsAuthenticated) 
                Logout(authenticationManager);

            var identity = new ClaimsIdentity(new[] 
                        {
                            new Claim(ClaimTypes.Name, model.Email),
                            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, string.Format("{0} {1}", model.FirstName, model.LastName.ToUpperInvariant())),
                        },
                        DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.GivenName);

            authenticationManager.SignIn(new AuthenticationProperties{ IsPersistent = true}, identity);

        }

        public void Logout(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}