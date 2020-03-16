﻿namespace AuthService.Api.Models
{
    public class SignInRequestModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class SignOutRequestModel
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; } 
        
        public string Password { get; set; }
    }

    public class EditProfileRequestModel
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; } 
    }
}