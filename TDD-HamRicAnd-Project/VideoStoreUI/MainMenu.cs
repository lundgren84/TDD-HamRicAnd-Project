using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBusinessLayer;

namespace VideoStoreUI
{
    public enum CurrentState
    {
        RegisterCustomer, RentMovie, ReturnMovie, GetCustomer
    }
    public class SUTVideoStoreConsole
    {
        private IVideoStore _videoStore { get; set; }
        public SUTVideoStoreConsole(IVideoStore _videoStore)
        {
            this._videoStore = _videoStore;
        }
        public void StarMenu()
        {
            while (true)
            {
                ConsoleWrite.MainMenu();
                var key = new ConsoleKeyInfo();
                key = Console.ReadKey(true);
                var input = key.KeyChar;
                Console.Clear();
                switch (input)
                {
                    case '1':
                        RegisterCustomer();
                        break;
                    case '2':
                      //  GetCustomer();             
                        break;
                    case '3':
                      //  GetCustomers();                   
                        break;
                    case '4':
                        RentMovie();                    
                        break;
                    case '5':
                        ReturnMovie();
                        break;
                    case '6':
                        AddMovie();
                        break;
                    case '7':
                     //   GetMovies();  
                        break;
                    case '8':
                        return;
                    default:
                        ErrorInput();
                        break;
                }
            }
        }

        private void GetMovies()
        {
            throw new NotImplementedException();
        }

        private void GetCustomers()
        {
            throw new NotImplementedException();
        }

        private void AddMovie()
        {
            var flag = true;    
            var title = "";        
            while (flag)
            {
                var menuString = $@"
ADD NEW MOVIE.   Enter EXIT to Abort.

Movie title:
";
                Console.WriteLine(menuString);
                if (Abort(title = Console.ReadLine())) { Console.Clear(); return; }

                try
                {
                    _videoStore.AddMovie(new Movie(title, MovieGenre.Action));
                    flag = false;
                }
                catch (MovieTitelsIsNullOrEmptyExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (MovieTitleOverloadExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }              
            }
            Console.Clear();
         

            ConsoleWrite.Success("Add Movie Succeded: " + title+" Genre: "+"");
        }

        private void ErrorInput()
        {
            var menuString = $@"";

            Console.WriteLine(menuString);
        }

        private void GetCustomer()
        {
            var menuString = $@"";

            Console.WriteLine(menuString);
        }

        private void ReturnMovie()
        {
            var flag = true;
            var lateReturn = false;
            var title = "";
            var ssn = "";
            while (flag)
            {
                var menuString = $@"
RETURN MOVIE.   Enter EXIT to Abort.

Movie title:
";
                Console.WriteLine(menuString);
                if (Abort(title = Console.ReadLine())) { Console.Clear(); return; }
                Console.WriteLine("Customer SocialSecurityNumber: ");
                if (Abort(ssn = Console.ReadLine())) { Console.Clear(); return; }
                try
                {
                    _videoStore.ReturnMovie(title, ssn);
                    flag = false;
                }
                catch (InvalidSocialSecurityNumberExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (CustomerDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (MovieDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (LateRentalExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                    lateReturn = true;
                }
            }
            Console.Clear();
            if (lateReturn) ConsoleWrite.Error("Late Return!");

            ConsoleWrite.Success("Return Succeded! Movie: " + title + ". Customer: " + ssn);
        }

        private void RentMovie()
        {
            var flag = true;
            var title = "";
            var ssn = "";
            while (flag)
            {
                var menuString = $@"
RENT MOVIE.   Enter EXIT to Abort.

Movie title:
";
                Console.WriteLine(menuString);
                if (Abort(title = Console.ReadLine())) { Console.Clear(); return; }
                Console.WriteLine("Customer SocialSecurityNumber: ");
                if (Abort(ssn = Console.ReadLine())) { Console.Clear(); return; }
                try
                {
                    _videoStore.RentMovie(title, ssn);
                    flag = false;
                }
                catch (InvalidSocialSecurityNumberExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (CustomerDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (MovieDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
               
            }
            Console.Clear();
            ConsoleWrite.Success("Rent Succeded! Movie: " + title + ". Customer: " + ssn);

        }

        private void RegisterCustomer()
        {
            var flag = true;
            var name = "";
            var ssn = "";
            while (flag)
            {
                var menuString1 = $@"


EnterName:
";
                ConsoleWrite.Heading("REGISTER NEW CUSTOMER.   Enter EXIT to Abort.");
                Console.WriteLine(menuString1);
                name = Console.ReadLine();
                if (Abort(name))
                    return;
                Console.WriteLine("Enter SocialSecurityNumber: (YYY-MM-DD)");
                ssn = Console.ReadLine();
                if (Abort(ssn))
                    return;
                try
                {
                    _videoStore.RegisterCustomer(ssn, name);
                    flag = false;
                }
                catch (InvalidSocialSecurityNumberExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (NameNullOrEmptyExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                catch (CustomerExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
            }
            Console.Clear();
            ConsoleWrite.Success("Registration Succeded! Name: " + name + ". SocialSecurityNumber: " + ssn);
        }
        public bool Abort(string input)
        {
            if (input.ToLower() == "exit")
                return true;
            else
                return false;
        }
    }

}
