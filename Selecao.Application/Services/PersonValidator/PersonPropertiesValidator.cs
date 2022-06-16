using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Selecao.Application.Services.PersonValidator
{
    public class PersonPropertiesValidator : IPersonPropertiesValidator
    {
        public const string phoneRegex = @"^[0-9]{5}\-[0-9]{4}";

        public bool PersonIsValid(string name, string lastName, string phoneNumber)
        {
            if (PersonNameIsValid(name) && PersonLastNameIsValid(lastName) && PersonPhoneNumberIsValid(phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool PersonNameIsValid(string name)
        {
            if (name.Length > 2) return true;
            else return false;
        }

        private static bool PersonLastNameIsValid(string lastName)
        {
            if(lastName.Length > 2) return true;
            else return false;
        }

        private static bool PersonPhoneNumberIsValid(string phoneNumber)
        {
            if (phoneNumber != null) return Regex.IsMatch(phoneNumber, phoneRegex);
            else return false;
        }
    }
}
