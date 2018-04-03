using System.Data.Entity;

namespace DABHandin2SQL
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<AdressList> AdressLists { get; set; }
        public DbSet<AdressInformation> AdressInformations { get; set; }
        public DbSet<AlternativeContactadress> AlternativeContactadresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }
}