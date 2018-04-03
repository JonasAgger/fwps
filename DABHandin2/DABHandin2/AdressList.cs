using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DABHandin2SQL
{
    public class AdressList
    {
        [Key]
        public int ID { get; set; }
        public uint PostNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public AdressList(uint postnumber, string city, string country)
        {
            PostNumber = postnumber;
            City = city;
            Country = country;
        }
    }
}