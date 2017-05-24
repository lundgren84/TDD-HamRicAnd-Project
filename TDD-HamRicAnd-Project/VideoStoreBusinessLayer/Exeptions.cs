using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class MovieDontExistsExeption : Exception
    {
        public MovieDontExistsExeption(string message) : base(message)
        {

        }
    }
    public class CustomerExistsExeption: Exception
    {
        public CustomerExistsExeption(string message) : base(message)
        {

        }
    }
    public class InvalidSocialSecurityNumberExeption : Exception
    {
        public InvalidSocialSecurityNumberExeption(string message) : base(message)
        {

        }
    }
    public class MovieTitelsIsNullOrEmptyExeption : Exception
    {
        public MovieTitelsIsNullOrEmptyExeption(string message) : base(message)
        {

        }
    }
    public class MovieTitleOverloadExeption : Exception
    {
        public MovieTitleOverloadExeption(string message) : base(message)
        {

        }
    }
}
