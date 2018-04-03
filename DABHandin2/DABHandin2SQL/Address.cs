using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DABHandin2SQL
{
    public class Address : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AddressType { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        public virtual City City { get; set; }
        public virtual List<Person> Persons { get; set; } = new List<Person>();
    }

    public class City : Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string ZipCode { get; set; }

    }
}