using System;
using System.Collections.Generic;

#nullable disable

namespace Recruiter.Models
{
    public partial class AnnualSalary
    {
        public string CEmployeeCode { get; set; }
        public decimal? MAnnualSalary { get; set; }
        public short SiYear { get; set; }

        public virtual Employee CEmployeeCodeNavigation { get; set; }
    }
}
