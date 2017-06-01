using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class VideoStore : IVideoStore
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        private IRentals Rentals { get; set; }
        public Dictionary<string, string> Customers { get; set; } = new Dictionary<string, string>();

        public VideoStore(IRentals rentals)
        {
            this.Rentals = rentals; 
        }

        public Dictionary<string, string> FillCustomers()
        {
            var customers = new Dictionary<string, string>();
            customers.Add("1111-11-11", "Olle Svensson");
            return Customers;
        }

        public void AddMovie(Movie movie)
        {
           if (ValidateMovie(movie))
            {          
                    Movies.Add(movie);
            }    
        }    

        public List<Customer> GetCustomers()
        {
            return Customers.ToCustomerList();
        }

        public void RegisterCustomer( string socialSecurityNumber, string name)
        {
           if (string.IsNullOrEmpty(name))
                throw new NameNullOrEmptyExeption(ExeptionMessages.NameNullOrEmptyExeptionMessage);

           if(StaticHelp.ValidateSocialSecurityNumber(socialSecurityNumber))
            {
                if (Customers.ContainsKey(socialSecurityNumber))
                {
                    throw new CustomerExistsExeption(ExeptionMessages.CustomerExistsExeptionMessage);
                }
                else
                    Customers.Add(socialSecurityNumber, name);
            }
           
        }

    

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            StaticHelp.ValidateSocialSecurityNumber(socialSecurityNumber);
            ValidateRental(movieTitle, socialSecurityNumber);
            Rentals.AddRental(movieTitle, socialSecurityNumber);
        }

        private void ValidateRental(string movieTitle, string socialSecurityNumber)
        {
            //Check if Customer exists
            Customers.CheckCustomerExsistence(socialSecurityNumber);   
      
            //Check if movie exists
            var movieToRent = Movies.FirstOrDefault(x => x.Title == movieTitle) ?? throw new MovieDontExistsExeption("Rental failed. "+ExeptionMessages.MovieDontExistsExeptionMessage);
        }


        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            //Validate SSN
            StaticHelp.ValidateSocialSecurityNumber(socialSecurityNumber);
            //Check if Customer exists
            Customers.CheckCustomerExsistence(socialSecurityNumber);
            //Check Movie      
            var rental =Rentals.GetRentalsFor(socialSecurityNumber)?.FirstOrDefault(x => x._movieTitle == movieTitle) ??  throw new MovieDontExistsExeption("Return failed. "+ExeptionMessages.MovieDontExistsExeptionMessage);

         
            Rentals.RemoveRental(movieTitle, socialSecurityNumber);

            if (rental._dueDate < DateTime.Now)
            {
                throw new LateRentalExeption(ExeptionMessages.LateRentalExeptionMessage);
            }
        }

        // Private Methods
        private bool ValidateMovie(Movie movie)
        {
            // Title Shud not be null
            var movieResult = string.IsNullOrEmpty(movie.Title) ? 
                throw new MovieTitelsIsNullOrEmptyExeption(ExeptionMessages.MovieTitelsIsNullOrEmptyExeptionMessage) : true;
            var movieCount = Movies.Where(x => x.Title == movie.Title).Count();

            // Cant have more then 3 of same movie
            if(movieCount >= 3)
            {
                throw new MovieTitleOverloadExeption(ExeptionMessages.MovieTitleOverloadExeptionMessage);          
            }
            return movieResult;
        }
        public List<Movie> FillMovieStorage()
        {
            var storage = new List<Movie>();
            storage.Add(new Movie("Die hard", MovieGenre.Action));
            storage.Add(new Movie("Titanic", MovieGenre.Drama));
            storage.Add(new Movie("The mask", MovieGenre.Comedy));
            storage.Add(new Movie("Zombie attack", MovieGenre.Horror));
            return storage;
        }
    

    }
}
