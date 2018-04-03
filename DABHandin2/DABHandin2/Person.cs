using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Dynamic;

namespace DABHandin2SQL
{
    public enum Type {
    Student,
    Worker
};

    public class Person
    {
        [Key] public int ID { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public Type Type { get; set; }
        public AdressInformation PrimaryContactadress { get; set; }
        public List<AlternativeContactadress> AlternativeContactadresses { get; set; }
        public Person(string firstname, string middlename, string lastname, Type type, AdressInformation primaryContactadress)
        {
            Firstname = firstname ?? "Hans";
            Middlename = middlename ?? "Munker";
            Lastname = lastname ?? "Larsen";
            Type = type;
            PrimaryContactadress = primaryContactadress;
        }
    }
}
