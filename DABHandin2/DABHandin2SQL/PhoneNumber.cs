using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DABHandin2SQL;

namespace DABHandin2SQL
{
    public enum PhoneType
    {
        Work,
        Home,
        Mobile
    }

    public class PhoneNumber : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public PhoneType Type { get; set; } = PhoneType.Mobile;
        public string Company { get; set; }
        public virtual List<Person> Persons { get; set; } = new List<Person>();
    }
}