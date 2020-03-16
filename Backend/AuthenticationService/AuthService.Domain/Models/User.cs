using System;

namespace AuthService.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; }


        public void Edit(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}