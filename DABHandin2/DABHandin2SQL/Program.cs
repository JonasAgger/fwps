using System;
using System.Linq;
using DABHandin2SQL;
using Type = DABHandin2SQL.Type;

namespace DABHandin2SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Context())
            {
                // Making Repositories 
                var personRepo = new Repository<Person>(db);
                var addressRepo = new Repository<Address>(db);
                var emailRepo = new Repository<Email>(db);
                var phoneRepo = new Repository<PhoneNumber>(db);
                var cityRepo = new Repository<City>(db);

                
                City city = new City() {CityName = "Aarhus", ZipCode = "8000"};
                cityRepo.Create(city);

                Address addr = new Address() { AddressType = "Lejlighed", City = city, HouseNumber = "5.3.2", StreetName = "Møllehatten"};
                Person person = new Person() { FirstName = "Jonas", LastName = "Agger", Type = Type.Student };

                // Adding to repo
                addressRepo.Create(addr);
                personRepo.Create(person);
                // Adding address to person
                person.Addresses.Add(addr);

                // Update person after address
                personRepo.Update(person.Id, person);

                // Creating email & number
                Email mail = new Email() { MailAddress = "Jonasagger@hotmail.com" };
                PhoneNumber phoneNumber = new PhoneNumber() {Company = "Callme", Number = "41144019"};
                emailRepo.Create(mail);
                phoneRepo.Create(phoneNumber);

                // Add email & number to person
                person.Emails.Add(mail);
                person.PhoneNumers.Add(phoneNumber);
                // Update person repository
                personRepo.Update(person.Id, person);


                var samePerson = personRepo.Read(person.Id);

                // Check data for person
                Console.WriteLine(samePerson.FirstName ?? "");
                Console.WriteLine(samePerson.LastName ?? "");
                Console.WriteLine(samePerson.Type);
                Console.WriteLine(samePerson.Emails.Count > 0 ? samePerson.Emails.FirstOrDefault().MailAddress : "No EmailAddress");
                Console.WriteLine(samePerson.PhoneNumers.Count > 0 ? samePerson.PhoneNumers.FirstOrDefault().Number : "No Phonenumber");
                Console.WriteLine(samePerson.Addresses.Count > 0 ? samePerson.Addresses.FirstOrDefault().StreetName : "No Street Entered");
                Console.WriteLine((samePerson.Addresses.Count > 0 && (samePerson.Addresses.FirstOrDefault().City != null)) ?
                                  samePerson.Addresses.FirstOrDefault().City.CityName : "No City Entered");

            }
        }
    }
}
