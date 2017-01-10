using RestWebAppService.Helpers;
using RestWebAppService.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppService.ViewModels
{
    public partial class Employee
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
        [Display(Name = "Datum narození")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public System.DateTime D_Narozeni { get; set; }

        [Required]
        [Display(Name = "Typ")]
        public EmployeeType Typ { get; set; }

        [Required]
        [Display(Name = "Výška[m]")]
        public decimal Vyska { get; set; }
    }
}
