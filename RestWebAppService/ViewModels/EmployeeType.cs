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
}
