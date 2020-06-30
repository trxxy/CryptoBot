using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using DataAccess.Entities;
using Logic.DataTableObjects;
using Logic.Services.Interfaces;
using System;

namespace Logic.Services
{
    public class AuthService : IAuthService
    {
        IUnitOfWork _db;
        IMapper _mapper;
        public UserDTO CurrentUser { get; set; }

        public AuthService(IUnitOfWork uow, 
            IMapper mapper)
        {
            _db = uow;
            _mapper = mapper;
        }

        public AuthState Login(string email, string password)
        {
            FindUserByEmail(email);
            if (CurrentUser != null)
            {
                if (CurrentUser.Password == password)
                {
                    return AuthState.LoginSuccess;
                }
                return AuthState.InvalidPassword;
            }
            return AuthState.InvalidEmail;
        }

        public AuthState Registration(string email, string password)
        {
            FindUserByEmail(email);
            if (CurrentUser != null)
            {
                return AuthState.AlreadyRegistred;
            }

            var newUser = new User()
            {
                Email = email,
                Password = password
            };

            _db.UserRepository.Create(newUser);
            _db.SaveChanges();
            FindUserByEmail(email);

            _db.Dispose();
            return AuthState.RegistrationSuccess;
        }

        public void UpdateUserData(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _db.UserRepository.Update(user);
        } 
        private void FindUserByEmail(string email)
        {
            User userEntity = _db.UserRepository.Get(q => q.Email == email);
            CurrentUser = _mapper.Map<UserDTO>(userEntity);
        }
    }
}