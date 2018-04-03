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
                var AddressRepo = new Repository<Address>(db);
                var emailRepo = new Repository<Email>(db);
                var phoneRepo = new Repository<PhoneNumber>(db);
                var cityRepo = new Repository<City>(db);

                var person = personRepo.Read(1);

                Console.WriteLine(person.FirstName);
                Console.WriteLine(person.LastName);
                Console.WriteLine(person.Emails.FirstOrDefault().MailAddress);
                Console.WriteLine(person.Addresses.FirstOrDefault().StreetName);
                Console.WriteLine(person.Addresses.FirstOrDefault().HouseNumber);
                Console.WriteLine(person.Addresses.FirstOrDefault().City.CityName);


                /*
                City city = new City() {CityName = "Aarhus", ZipCode = "8000"};
                cityRepo.Create(city);

                Address addr = new Address() { AddressType = "Lejlighed", City = city, HouseNumber = "5.3.2", StreetName = "Møllehatten"};
                //Add person to list
                Person person = new Person() { FirstName = "Jonas", LastName = "Agger" };

                // Adding to repo
                AddressRepo.Create(addr);
                personRepo.Create(person);
                // Adding to person
                person.Addresses.Add(addr);

                // Update person
                personRepo.Update(person.Id, person);

                // Creating email
                Email mail = new Email() { MailAddress = "Jonasagger@hotmail.com" };
                emailRepo.Create(mail);

                // Add email to person
                person.Emails.Add(mail);
                // Update person repository
                personRepo.Update(person.Id, person);

                //emailRepo.Delete(mail);

                //Console.Read();
                */
            }
        }
    }
}
