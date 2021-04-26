using Ninject;
using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatBot.Business.Services.Models
{
    public class CustomEmailValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _userRepository =new UserRepository(new Data.EntityFramework.ChatBotContext());
           

            if (value != null)
            {
                string email = value.ToString();
                var isExists = _userRepository.CheckEmailIdExists(email);                

                if (!isExists)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Email address already exists.");
                }
            }

            return new ValidationResult("" + validationContext.DisplayName + " is required.");
        }
    }
}