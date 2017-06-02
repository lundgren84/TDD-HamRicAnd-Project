using System;

namespace VideoStoreBusinessLayer
{

    public class NameNullOrEmptyExeption : Exception
    {
        public NameNullOrEmptyExeption(string message) : base(message)
        {

        }
    }
    public class CustomerDontExistsExeption : Exception
    {
        public CustomerDontExistsExeption(string message) : base(message)
        {

        }
    }
    public class MovieDontExistsExeption : Exception
    {
        public MovieDontExistsExeption(string message) : base(message)
        {

        }
    }
    public class CustomerExistsExeption : Exception
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
    public class RentalOverloadExeption : Exception
    {
        public RentalOverloadExeption(string message) : base(message)
        {

        }
        public RentalOverloadExeption()
        {

        }
    }
    public class ForbidenRentalExeption : Exception
    {
        public ForbidenRentalExeption(string message) : base(message)
        {

        }
        public ForbidenRentalExeption()
        {

        }
    }
    public class LateRentalExeption : Exception
    {
        public LateRentalExeption(string message) : base(message)
        {

        }
        public LateRentalExeption()
        {

        }
    }
    public static class ExeptionMessages
    {
        public const string CustomerDontExistsExeptionMessage = "Customer dont exists.";
        public const string MovieDontExistsExeptionMessage = "Movie dont exists.";
        public const string CustomerExistsExeptionMessage = "Customer with that SocialSecurityNumber allready exists. Registry failed.";
        public const string InvalidSocialSecurityNumberExeptionMessage = "Invalid SocialSecurityNumber. Use YYYY-MM-DD";
        public const string MovieTitelsIsNullOrEmptyExeptionMessage = "Movie title cant be null or empty. Need a string";
        public const string MovieTitleOverloadExeptionMessage = "You cant add more movies with that title. (Max 3 Copies of same title)";
        public const string RentalOverloadExeptionMessage = "Customers can maximum have 3 active rentals.";
        public const string ForbidenRentalExeptionMessage = "Not allowed to rent same Movie-title twice.";
        public const string LateRentalExeptionMessage = "Return late rentals to enable new rentals.";
        public const string NameNullOrEmptyExeptionMessage = "Name can not be null or empty.";
        public const string LateReturnExeptionMessage = "Movie Returned After Due-Date!";


    }
}
