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
        public List<Movie> Movies { get; set; }
        private IRentals Rentals { get; set; }
        public Dictionary<string, string> Customers { get; set; } 
        private Regex SsnRegex = new Regex(@"^\d{4}-\d{2}-\d{2}$");

        public VideoStore(IRentals rentals)
        {
            this.Rentals = rentals;
            this.Movies = FillMovieStorage();
            this.Customers = new Dictionary<string, string>();
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

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            if (Customers.ContainsKey(socialSecurityNumber))
            {
                throw new CustomerExistsExeption("Customer with SocialSecurityNumber: "+ socialSecurityNumber+" allready exists. Registry failed.");
            }
            else if (!SsnRegex.IsMatch(socialSecurityNumber))
            {
                throw new InvalidSocialSecurityNumberExeption("Invalid SocialSecurityNumber. Use YYYY-MM-DD");
            }
           else
                Customers.Add(socialSecurityNumber, name);   
        }    

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            ValidateRental(movieTitle, socialSecurityNumber);
        }

        private void ValidateRental(string movieTitle, string socialSecurityNumber)
        {
            //Check if Customer exists
            try
            {
                var customer = Customers[socialSecurityNumber];
            }
            catch (KeyNotFoundException)
            {

                throw new CustomerDontExistsExeption("Customer dont exists.");
            }
            //Check if movie exists
            var movieToRent = Movies.FirstOrDefault(x => x.Title == movieTitle) ?? throw new MovieDontExistsExeption("Movie dont exists.");
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        // Private Methods
        private bool ValidateMovie(Movie movie)
        {
            // Title Shud not be null
            var movieResult = string.IsNullOrEmpty(movie.Title) ? 
                throw new MovieTitelsIsNullOrEmptyExeption("Movie title cant be null or empty. Need a string") : true;
            var movieCount = Movies.Where(x => x.Title == movie.Title).Count();

            // Cant have more then 3 of same movie
            if(movieCount >= 3)
            {
                throw new MovieTitleOverloadExeption("You cant add more movies with title: "+movie.Title+" (Max 3 Copies of same title)");          
            }
            return movieResult;
        }
        private static List<Movie> FillMovieStorage()
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
