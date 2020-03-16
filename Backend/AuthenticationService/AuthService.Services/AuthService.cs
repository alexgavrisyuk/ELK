using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Domain.Models;
using AuthService.Infrastructure;
using AuthService.Services.Helper;
using CryptSharp;

namespace AuthService.Services
{
    public class AuthService
    {

        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> SignInAsync(string login, string password)
        {
            var user = await _userRepository.FindAsync(login);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            var token = JwtTokenUtil.Generate(user.Email);
            return token;
        }

        public async Task<string> SignOutAsync(string email, string firstName, string lastName, int age,
            string password)
        {
            var user = await _userRepository.FindAsync(email);
            if (user != null)
            {
                throw new Exception("User with same login already exist exist");
            }

            var newUser = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                PasswordHash = Crypter.Sha512.Crypt(password)
            };
            
            await _userRepository.CreateAsync(newUser);

            var token = JwtTokenUtil.Generate(newUser.Email);
            return token;
        }
    }
}