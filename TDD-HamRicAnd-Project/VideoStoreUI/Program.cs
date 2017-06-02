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
            var date = new DateTimes();
            var rentals = new Rentals(date);
         
            var VidStore = new VideoStore(rentals);          
            VidStore.Movies = VidStore.FillMovieStorage();
            SUTVideoStoreConsole VideoStore = new SUTVideoStoreConsole(VidStore,rentals);
            VideoStore.StarMenu();
        }
    }
}
