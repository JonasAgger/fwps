using System.Data.Entity;
using DABHandin2SQL;

namespace DABHandin2SQL
{
    public class Context : DbContext
    {
        public Context()
            : base()
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> PublicAddresses { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}