//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestWebAppService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int Id { get; set; }
        public string Prijmeni { get; set; }
        public string Jmeno { get; set; }
        public System.DateTime D_Narozeni { get; set; }
        public short Typ { get; set; }
        public decimal Vyska { get; set; }
    }
}