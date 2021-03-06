﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestWebAppClient.Models
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
        [Display(Name = "Typ zaměstnance")]
        [JsonIgnore]
        public EmployeeType EmployeeType
        {
            get
            {
                return (EmployeeType)Typ;
            }
            set
            {
                Typ = (short)value;
            }
        }

        [Required]
        [Display(Name = "Výška")]
        public decimal Vyska { get; set; }
    }
}
