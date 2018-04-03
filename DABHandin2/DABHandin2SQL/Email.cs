using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DABHandin2SQL;

namespace DABHandin2SQL
{
    public class Email : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MailAddress { get; set; }
        public virtual List<Person> Persons { get; set; } = new List<Person>();
    }
}