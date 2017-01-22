using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppService.ViewModels
{
    public enum EmployeeType
    {
        [Display(Name = "Ředitel")]
        Reditel,
        [Display(Name = "Vedoucí")]
        Vedouci,
        [Display(Name = "Ostatní")]
        Ostatni
    }
    public class Employee
    {
        public void Read(RestWebAppService.Models.Employee employee)
        {
            if (employee != null)
            {
                Id = employee.Id;
                Prijmeni = employee.Prijmeni;
                Jmeno = employee.Jmeno;
                D_Narozeni = employee.D_Narozeni;
                EmployeeType = (EmployeeType)employee.Typ;
                Vyska = employee.Vyska;
            }
        }

        public void Write(RestWebAppService.Models.Employee employee)
        {
            if (employee != null)
            {
                employee.Id = Id;
                employee.Prijmeni = Prijmeni;
                employee.Jmeno = Jmeno;
                employee.D_Narozeni =D_Narozeni;
                employee.Typ = (short)EmployeeType;
                employee.Vyska = Vyska;
            }
        }

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
        [Display(Name = "Typ zaměstnance")]
        [JsonIgnore]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        [Display(Name = "Výška")]
        public decimal Vyska { get; set; }
    }
}
