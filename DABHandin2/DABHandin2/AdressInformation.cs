using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DABHandin2SQL
{
    public class AdressInformation : IContactInfo
    {
        [Key]
        public int ID { get; set; }

        public string RoadName { get; set; }
        public string HouseNumber { get; set; }
        public AdressList AdressList { get; set; }

        public AdressInformation(string roadname, string housenumber, AdressList adresslist)
        {
            RoadName = roadname;
            HouseNumber = housenumber;
            AdressList = adresslist;
        }


        public string GetInfo()
        {
            var sb = new StringBuilder();

            sb.Append(AdressList.Country);
            sb.Append(", ");
            sb.Append(AdressList.City);
            sb.Append(", ");
            sb.Append(AdressList.PostNumber);
            sb.Append(", ");
            sb.Append(RoadName);
            sb.Append(" ");
            sb.Append(HouseNumber);

            return sb.ToString();
        }
    }
}