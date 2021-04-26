using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatBot.Models
{
    public class ChangePasswordModel
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "Old Password is required.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email address.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email address.")]
        public string EmailAddress { get; set; }
        public string Message { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public long ContactNumber { get; set; }
        public string Address { get; set; }
    }
}