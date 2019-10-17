using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace owp_web.Models
{
    public enum WorkItemPriority
    {
        [Display(Name = "Low")]
        Low = 0,
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "High")]
        High = 2,
        [Display(Name = "Critical")]
        Critical = 3
    }
}
