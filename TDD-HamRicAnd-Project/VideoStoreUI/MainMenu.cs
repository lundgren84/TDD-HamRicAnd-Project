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
                        RentMovie();
                        break;
                    case '3':
                        ReturnMovie();
                        break;
                    case '4':
                        AddMovie();
                        break;
                    case '5':
                        GetCustomer();
                        break;
                    case '6':
                        break;
                    default:
                        ErrorInput();
                        break;
                }
            }
        }

        private void AddMovie()
        {
            throw new NotImplementedException();
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
                if (Abort(title = Console.ReadLine())) return;
                Console.WriteLine("Customer SocialSecurityNumber: ");
                if (Abort(ssn = Console.ReadLine())) return;
                try
                {
                    _videoStore.ReturnMovie(title, ssn);
                    flag = false;
                }
                catch (InvalidSocialSecurityNumberExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (CustomerDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (MovieDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (LateRentalExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
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
                if (Abort(title = Console.ReadLine())) return;
                Console.WriteLine("Customer SocialSecurityNumber: ");
                if (Abort(ssn = Console.ReadLine())) return;
                try
                {
                    _videoStore.RentMovie(title, ssn);
                    flag = false;
                }
                catch (InvalidSocialSecurityNumberExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (CustomerDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (MovieDontExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
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
REGISTER NEW CUSTOMER.   Enter EXIT to Abort.

EnterName:
";
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
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (NameNullOrEmptyExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
                }
                catch (CustomerExistsExeption ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.ToString());
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
