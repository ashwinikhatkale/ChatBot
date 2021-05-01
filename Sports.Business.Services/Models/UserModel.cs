using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Models
{
   public class UserModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "First Name is required.")]        
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

        [StringLength(10, MinimumLength = 0,ErrorMessage="Contact Number must be less than 10 digits.")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Passward is required.")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "Password must be less than 10 digits.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Passward is required.")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "Confirm Password must be less than 10 digits.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email address.")]
        [Remote("IsEmailExist", "Register", ErrorMessage = "User with same email address already exists.")]
        [CustomEmailValidator]
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
