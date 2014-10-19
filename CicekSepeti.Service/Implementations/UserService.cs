using System;
using CicekSepeti.Entity;
using CicekSepeti.Facility;
using CicekSepeti.Model;
using CicekSepeti.Repository.Contracts;
using CicekSepeti.Repository.Implementations;
using CicekSepeti.Service.Contracts;
using FastMapper;
using Microsoft.Owin.Security;

namespace CicekSepeti.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;
        private readonly IUnitOfWork _uow;

        public UserService(UserRepository userRepo, IUnitOfWork uow)
        {
            _userRepo = userRepo;
            _uow = uow;
        }

        public UserDTO GetUser(string email, string hashedPassword)
        {
            var user = _userRepo.Get(a => a.Email.ToLowerInvariant().Equals(email.ToLowerInvariant()) && a.HashedPassword.Equals(hashedPassword));

            return TypeAdapter.Adapt<User, UserDTO>(user);
        }

        public ResultModel Register(NewUserViewModel model)
        {
            var result = new ResultModel();

            if (IsEmailExist(model.Email))
            {
                result.IsSuccess = false;
                result.Message = Facilities.GetDescription(GlobalMessages.EmailIsInUse);
            }
            else
            {
                var registrationResult = RegisterUser(model);

                if (!registrationResult.IsSuccess)
                {
                    result.IsSuccess = false;
                    result.Message = registrationResult.Message;
                }

                result.Data = registrationResult.Data;
            }

             return result;
        }

        private ResultModel RegisterUser(NewUserViewModel model)
        {
            var result = new ResultModel();

            var salt = Facilities.GenerateSalt(50, false);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordSalt = salt,
                HashedPassword = Facilities.GeneratePassword(model.Password, salt)
            };

            _userRepo.Add(user);

            if (!_uow.Commit())
            {
                result.IsSuccess = false;
                result.Message=Facilities.GetDescription(GlobalMessages.ErrorWhileTransaction);
            }

            result.Data = TypeAdapter.Adapt<User, UserDTO>(user);

            CreateActivity(user, ref result);

            return result;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepo.Get(a => a.Email.ToLowerInvariant().Equals(email.ToLowerInvariant())) != null;
        }

        public ResultModel Login(LoginViewModel model)
        {
           var result = new ResultModel();

            CheckIfUserExist(model, ref result);

            return result;
        }

        private void CheckIfUserExist(LoginViewModel model, ref ResultModel result)
        {

            var user = _userRepo.Get(a => a.Email.ToLowerInvariant().Equals(model.Email.ToLowerInvariant()));

            if (user == null)
            {
                result.IsSuccess = false;
                result.Message = Facilities.GetDescription(GlobalMessages.EmailIsNotExist);
            }
            else
            {
                var hashedPassword = Facilities.GeneratePassword(model.Password, user.PasswordSalt);

                if (user.HashedPassword != hashedPassword)
                {
                    result.IsSuccess = false;
                    result.Message = Facilities.GetDescription(GlobalMessages.PasswordIsNotCorrect);
                }
                else
                {
                    result.Data = TypeAdapter.Adapt<User, UserDTO>(user);
                    CreateActivity(user, ref result);
                }

            }
        }

        private void CreateActivity(User user, ref ResultModel result)
        {
            var activity = new Activity()
            {
                LoginDateTime = DateTime.Now, 
                User = user
            };

            user.Activities.Add(activity);
            _userRepo.Update(user);

            if (!_uow.Commit())
            {
                result.IsSuccess = false;
                result.Message = Facilities.GetDescription(GlobalMessages.ErrorWhileTransaction);
            }

        }
    }
}