using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class CustomerExistsExeption: Exception
    {
        public CustomerExistsExeption(string message) : base(message)
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
}
