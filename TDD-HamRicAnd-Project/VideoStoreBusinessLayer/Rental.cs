using System;
using System.Collections.Generic;

namespace VideoStoreBusinessLayer
{
    public class Rental : IRental
    {
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}