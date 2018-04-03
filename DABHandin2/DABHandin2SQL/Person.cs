using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DABHandin2SQL;

namespace DABHandin2SQL
{
    public enum Type
    {
        Student,
        Worker
    };

    public class Person : Entity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Type Type { get; set; } = Type.Worker;

        public virtual List<Email> Emails { get; set; } = new List<Email>();
        public virtual List<PhoneNumber> PhoneNumers { get; set; } = new List<PhoneNumber>();
        public virtual List<Address> Addresses { get; set; } = new List<Address>();
    }
}