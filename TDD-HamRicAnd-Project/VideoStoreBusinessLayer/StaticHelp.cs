using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
   public static class StaticHelp
    {
        public static Regex SsnRegex = new Regex(@"^\d{4}-\d{2}-\d{2}$");

        public static bool ValidateSocialSecurityNumber(string socialSecurityNumber)
        {
            if (!SsnRegex.IsMatch(socialSecurityNumber))
            {
                throw new InvalidSocialSecurityNumberExeption(ExeptionMessages.InvalidSocialSecurityNumberExeptionMessage);
            }
            return true;
        }

    }
}
