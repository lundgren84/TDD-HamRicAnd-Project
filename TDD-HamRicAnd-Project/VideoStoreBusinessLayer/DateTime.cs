using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLayer
{
    public class DateTimes : IDateTime
    {
        public DateTime Now()
        {
          
            return DateTime.Now;
        }
    }
}
