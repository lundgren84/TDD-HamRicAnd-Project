using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBusinessLayer;

namespace VideoStoreUI
{
    class Program
    {
        static void Main(string[] args)
        {        
            var rentals = new Rentals(new DateTimes());
            var VidStore = new VideoStore(rentals);
            VidStore.Customers = VidStore.FillCustomers();
            VidStore.Movies = VidStore.FillMovieStorage();
            SUTVideoStoreConsole VideoStore = new SUTVideoStoreConsole(VidStore);
            VideoStore.StarMenu();
        }
    }
}
