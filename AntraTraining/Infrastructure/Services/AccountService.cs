using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repositories;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public async Task<UserLoginResponseModel> LoginUser(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if(dbUser == null)
            {
                throw new Exception("Email does not exist");
            }
            var hashedPassword = GetHashedPassword(password, dbUser.Salt);
            if (hashedPassword == dbUser.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault()
                };

                return userLoginResponseModel;
                
            }
            return null;
        }
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                throw new ConflictException("Email already exists");
            }

            var salt = GetRandomSalt();
            var hashedPassword = GetHashedPassword(model.Password, salt);

            var user = new User
            {
                Email = model.Email,
            Salt = salt,
            HashedPassword = hashedPassword,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth
            };

            var createdUser = await _userRepository.Add(user);
            if(createdUser.Id > 1)
            {
                return true;
            }
            //create a random salt
            //create hashed password with the salt created above
            // Never ever create your own hashing algorithms
            //Pdbkf2, Bcrypt, Aargon2
            //save the user object


            return true;
        }

        //public async Task<bool> ValidateUser(string email, string password)
        //{
        //    var user = await _userRepository.GetUserByEmail(email);
        //    if (user == null)
        //        throw new Exception("Email does not exist");
            
        //    var hashedPassword = GetHashedPassword(password, user.Salt); 
        //    if(hashedPassword == user.HashedPassword)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
            
        //}

        public async Task<bool> CheckEmail(string email)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                return false;
            }
            return true;
        }
        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password,
           Convert.FromBase64String(salt),
           KeyDerivationPrf.HMACSHA512,
           10000,
           256 / 8));
            return hashed;
        }

    }
}
