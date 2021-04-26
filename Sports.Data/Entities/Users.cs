using System;

namespace ChatBot.Data.Entities
{
   public class Users : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public long RoleId { get; set; }
    }
}
