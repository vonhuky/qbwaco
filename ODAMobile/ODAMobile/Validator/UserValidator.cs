using FluentValidation;
using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Validator
{
    public class UserValidator : AbstractValidator<UserInfo>
    {
        public UserValidator()
        {
            RuleFor(c => c.Username).Must(n => ValidateStringEmpty(n)).WithMessage("Username should not be empty.");
            RuleFor(c => c.Password).Must(n => ValidateStringEmpty(n)).WithMessage("Password should not be empty.");
        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
    }
}
