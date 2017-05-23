using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class VideoStore : IVideoStore
    {
        public void AddMovie(Movie movie)
        {
            throw new NotImplementedException();
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
    }
}
