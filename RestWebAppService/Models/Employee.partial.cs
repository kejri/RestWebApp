using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppService.Models
{
    [MetadataType(typeof(Employee.MetaData))]
    public partial class Employee
    {
        internal class MetaData
        {
            [ScaffoldColumn(false)]//nebude nikde zobrazen
            public int Id { get; set; }

            [Required]
            [Display(Name = "Příjmení")]
            public string Prijmeni { get; set; }

            [Required]
            [Display(Name = "Jméno")]
            public string Jmeno { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "D. Narození")]
            [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime D_Narozeni { get; set; }

            [Required]
            [Display(Name = "Typ")]
            public short Typ { get; set; }

            [Required]
            [Display(Name = "Výška")]
            public decimal Vyska { get; set; }
        }
    }
}
