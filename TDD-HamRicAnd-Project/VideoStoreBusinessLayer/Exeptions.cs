using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    
   public class MovieTitleIsNullOrEmpty : Exception
    {
        public MovieTitleIsNullOrEmpty(string message) :base(message)
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
