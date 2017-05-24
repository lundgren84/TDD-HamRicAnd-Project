using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class VideoStore : IVideoStore
    {
        public List<Movie> Movies { get; set; } = FillMovieStorage();

      

        public void AddMovie(Movie movie)
        {
           if (ValidateMovie(movie))
            {
                    Movies.Add(movie);         
            }
      
        }

     

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        // Private Methods
        private bool ValidateMovie(Movie movie)
        {
            var movieResult = string.IsNullOrEmpty(movie.Title) ? 
                throw new MovieTitleIsNullOrEmpty("Movie title cant be null or empty. Need a string") : true;
            var movieCount = Movies.Where(x=> x.Title == movie.Title).ToList();

            if(movieCount.Count >= 3)
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
