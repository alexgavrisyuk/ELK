using System;
using System.Threading.Tasks;
using AuthService.Domain.Models;
using AuthService.Infrastructure;

namespace AuthService.Services
{
    public class ProfileService
    {
        private readonly UserRepository _userRepository;

        public ProfileService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await _userRepository.FindAsync(email);
            if (user == null)
            {
                throw new Exception($"User with email {email} does not exist");
            }

            return user;
        }

        public async Task<User> EditProfileAsync(string id, string email, string firstName, string lastName, int age)
        {
            var user = await _userRepository.FindAsync(email);
            if (user == null)
            {
                throw new Exception($"User with email {email} does not exist");
            }

            user.Edit(firstName, lastName, age);
            user = await _userRepository.UpdateAsync(user);
                
            return user;
        }
    }
}