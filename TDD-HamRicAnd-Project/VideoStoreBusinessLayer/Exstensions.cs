using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
   public static class Extensions
    {
        public static List<Customer> ToCustomerList(this Dictionary<string,string> Customers)
        {
          
            var customerList = new List<Customer>();
            foreach (var customer in Customers)
            {
                customerList.Add(new Customer(customer.Value, customer.Key));
            }
            return customerList;
        }
    }
}
