using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
   public class Rental
    {
        public DateTime _dueDate { get; set; }
        public string _movieTitle { get; set; }
        public string _customerSsn { get; set; }
    
        public Rental(DateTime dueDate, string movieTitle,string customerSsn)
        {
            _dueDate = dueDate;
            _movieTitle = movieTitle;
            _customerSsn = customerSsn;
        }
        public bool IsLate()
        {
            return _dueDate < DateTime.Now;
        }
      
    }
}
