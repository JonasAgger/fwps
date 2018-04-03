using System.ComponentModel.DataAnnotations;

namespace DABHandin2SQL
{
    public class Email : IContactInfo
    {
        [Key]
        public string _email { get; set; }

        public Email(string email)
        {
            this._email = email;
        }


        public string GetInfo()
        {
            return _email;
        }
    }
}