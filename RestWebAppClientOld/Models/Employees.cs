using Newtonsoft.Json;
using RestWebAppClient.Helpers;
using RestWebAppClient.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Models
{
    public class Employees
    {
        public Employee[] Result;
    }

    public class Employee
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int Id { get; set; }
        public string Prijmeni { get; set; }
        public string Jmeno { get; set; }
        public System.DateTime D_Narozeni { get; set; }
        public short Typ { get; set; }
        public decimal Vyska { get; set; }
    }
}
