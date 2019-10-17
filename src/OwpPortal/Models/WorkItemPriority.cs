using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace owp_web.Models
{
    public enum WorkItemPriority
    {
        [Display(Name = "0 - Low")]
        Low = 0,
        [Display(Name = "1 - Normal")]
        Normal = 1,
        [Display(Name = "2 - High")]
        High = 2,
        [Display(Name = "3 - Critical")]
        Critical = 3
    }
}
