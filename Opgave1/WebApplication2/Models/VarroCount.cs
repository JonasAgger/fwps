using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class VarroCount
    {
        [Key]
        public int ID { set; get; }
        public string Bistade { set; get; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { set; get; }
        public int MiteCount { set; get; }
        public int ObservationTime { set; get; }
        public string Comment { set; get; }

        public string User { set; get; }

        public string Postnummer { set; get; }

        public VarroCount()
        {}

        public VarroCount(string bistade, DateTime date, int count, string comment, int obstime, string uid, string postnummer)
        {
            this.Bistade = bistade;
            this.Date = date;
            this.MiteCount = count;
            this.Comment = comment;
            ObservationTime = obstime;
            User = uid;
            Postnummer = postnummer;
        }

    }

    public class VarroCountDTO
    {
        [Key]
        public int ID { set; get; }
        public string Bistade { set; get; }
        public string Date { set; get; }
        public int MiteCount { set; get; }
        public int ObsTime { set; get; }
        public string Comment { set; get; }

        public string User { set; get; }

        public string Postnummer { set; get; }

        public VarroCountDTO()
        { }

        public VarroCountDTO(string bistade, string date, int count, string comment, int obstime, string uid, string postnummer)
        {
            this.Bistade = bistade;
            this.Date = date;
            this.MiteCount = count;
            this.Comment = comment;
            ObsTime = obstime;
            User = uid;
            Postnummer = postnummer;
        }

        public VarroCount ToVarroCount()
        {
            return new VarroCount() {
                Bistade = Bistade,
                Date = DateTime.Parse(Date, CultureInfo.CurrentCulture),
                MiteCount = MiteCount,
                Comment = Comment,
                ObservationTime = ObsTime
            };
        }

    }
}