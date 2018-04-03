using System.ComponentModel.DataAnnotations;

namespace DABHandin2SQL
{
    public class PhoneNumber : IContactInfo
    {
        [Key]
        public string _number { get; set; }
        private readonly string _carrier;

        public PhoneNumber(string number, string carrier)
        {
            _number = number;
            _carrier = carrier;
        }

        public string GetInfo()
        {
            return "Number: " + _number + "\nCarrier: " + _carrier;
        }
    }
}