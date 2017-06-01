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
                customerList.Add(new Customer(customer.Key,customer.Value));
            }
            return customerList;
        }
        public static void CheckCustomerExsistence(this Dictionary<string,string> customers, string socialSecurityNumber)
        {
            try
            {
                var customer = customers[socialSecurityNumber];
            }
            catch (KeyNotFoundException)
            {
                throw new CustomerDontExistsExeption(ExeptionMessages.CustomerDontExistsExeptionMessage);
            }
        }
    }
   
}
