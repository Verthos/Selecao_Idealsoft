using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selecao.Application.Services.PersonValidator
{
    public class PersonPropertiesValidator : IPersonPropertiesValidator
    {


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
            return true;
        }

        private static bool PersonLastNameIsValid(string lastName)
        {
            return true;
        }

        private static bool PersonPhoneNumberIsValid(string phoneNumber)
        {
            return true;
        }
    }
}
