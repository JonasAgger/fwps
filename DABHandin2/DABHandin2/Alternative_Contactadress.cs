using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace DABHandin2SQL
{
    public enum Adresstype
    {
        Privat,
        Work,
        Vacationhouse
    }

    public class AlternativeContactadress
    {
        [Key] public int ID { get; set; }
        public Adresstype Adresstype { get; set; }
        public IContactInfo ContactInfo { get; set; }

        public AlternativeContactadress(IContactInfo contactInfo, Adresstype adresstype)
        {
            ContactInfo = contactInfo;
            Adresstype = adresstype;
        }
    }
}