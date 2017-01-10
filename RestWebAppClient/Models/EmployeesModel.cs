using Newtonsoft.Json;
using RestWebAppClient.Helpers;
using RestWebAppClient.Properties;
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
        [Display(Name = "Typ zeměstnance")]
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

    public class EmployeesModel
    {
        private readonly HttpSessionStateBase m_Session;
        public EmployeesModel(HttpSessionStateBase session)
        {
            m_Session = session;
        }
        public List<Employee> Employees
        {
            get
            {
                return m_Session["Employees"] as List<Employee>;
            }
            set
            {
                m_Session["Employees"] = value;
            }
        }

        public Employee Employee
        {
            get
            {
                return m_Session["Employee"] as Employee;
            }
            set
            {
                m_Session["Employee"] = value;
            }
        }

        /*private EmployeeType[] m_EmployeeTypes = EnumExtender.GetValues<EmployeeType>();
        public EmployeeType[] EmployeeTypes
        {
            get
            {
                return m_EmployeeTypes;
            }
        }*/

    }
}
