﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStoreBusinessLayer
{
    public class Rentals : IRentals
    {
        private List<Rental> _rentals { get; set; }
        private IDateTime _dateTime;
        public Rentals(IDateTime _dateTime)
        {
            this._dateTime = _dateTime;
            _rentals = new List<Rental>();
        }
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            StaticHelp.ValidateSocialSecurityNumber(socialSecurityNumber);
            ValidateRental(movieTitle.ToLower(), socialSecurityNumber);
            _rentals.Add(new Rental(_dateTime.Now().AddDays(3),movieTitle, socialSecurityNumber));
        }

        private void ValidateRental(string movieTitle, string socialSecurityNumber)
        {
            var rentals = _rentals.Where(x => x._customerSsn == socialSecurityNumber).ToList();
           if (rentals.Count()>= 3)
            {
                throw new RentalOverloadExeption(ExeptionMessages.RentalOverloadExeptionMessage);
            }
            foreach (var rental in rentals)
            {
                if (rental._movieTitle.ToLower() == movieTitle.ToLower())
                    throw new ForbidenRentalExeption(ExeptionMessages.ForbidenRentalExeptionMessage);
                if (rental.IsLate())
                    throw new LateRentalExeption(ExeptionMessages.LateRentalExeptionMessage);
            }
         
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return _rentals.Where(x=> x._customerSsn == socialSecurityNumber).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            StaticHelp.ValidateSocialSecurityNumber(socialSecurityNumber);
            var rentalToRemove = _rentals.FirstOrDefault(x => x._movieTitle.ToLower() == movieTitle.ToLower() &&
            x._customerSsn == socialSecurityNumber);

            _rentals.Remove(rentalToRemove);
        }
    }
}