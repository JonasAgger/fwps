using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DABHandin2SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating Enviroment
            var adrlist = new AdressList(8240, "Risskov", "DK");
            var adrinfo = new AdressInformation("Møllehatten", "5 3.2", adrlist);

            // Creating person
            var person = new Person("Jonas", "Agger", "Jørgensen",
                                    Type.Student, adrinfo);

            // Adding 2 alternative "addresses"
            person.AlternativeContactadresses.Add(
                new AlternativeContactadress(new Email("hallo@gmail.com"), Adresstype.Privat));

            person.AlternativeContactadresses.Add(
                new AlternativeContactadress(new PhoneNumber("41144017", "Telia"), Adresstype.Work));


            // Printing name information
            Console.WriteLine("Printing person information");
            Console.WriteLine("Firstname: {0}\nMiddlename: {1}\nLastname: {2}",
                              person.Firstname, person.Middlename, person.Lastname);

            // Printing primary contactinfo
            Console.WriteLine("\n\nPrinting primary Contactadress:");
            Console.WriteLine(person.PrimaryContactadress.GetInfo());

            //Printing alternate contactinfo if any exists, and it's type
            Console.WriteLine("\n\nPrinting Alternative Contactinfo");
            if (person.AlternativeContactadresses.Count < 1)
            {
                Console.WriteLine("No Alternative Contactinfo added");
            }
            else
            { 
                foreach (var addrinfo in person.AlternativeContactadresses)
                {
                    Console.WriteLine("Type: {0} {1}", 
                                addrinfo.Adresstype, addrinfo.ContactInfo.GetType().Name);

                    Console.WriteLine(addrinfo.ContactInfo.GetInfo()+'\n');
                }
                
            }
        }
    }
}
