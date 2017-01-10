﻿using RestWebAppClient.Helpers;
using RestWebAppClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Models
{
    public enum EmployeeType
    {
        [LocalizedEnum("EmployeeType_Reditel_Text", NameResourceType = typeof(Resources))]
        Reditel,
        [LocalizedEnum("EmployeeType_Vedouci_Text", NameResourceType = typeof(Resources))]
        Vedouci,
        [LocalizedEnum("EmployeeType_Ostatni_Text", NameResourceType = typeof(Resources))]
        Ostatni
    }
}
