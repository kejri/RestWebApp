using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppServer.Models
{
    [MetadataType(typeof(Employee.MetaData))]
    public partial class Employee
    {
        internal class MetaData
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Příjmení")]
            public string Prijmeni { get; set; }

            [Required]
            [Display(Name = "Jméno")]
            public string Jmeno { get; set; }

            [Required]
            [Display(Name = "Datum narození")]
            [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
            public System.DateTime D_Narozeni { get; set; }

            public short Typ { get; set; }

            [Required]
            [Display(Name = "Výška")]
            public decimal Vyska { get; set; }
        }
    }
}
