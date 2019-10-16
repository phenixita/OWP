using System.ComponentModel.DataAnnotations;

namespace owp_web.Models
{
    public enum WorkItemStatus
    {

        [Display(Name = "Created")]
        Created,

        [Display(Name = "Assigned")]
        Assigned,

        [Display(Name = "Resolved")]
        Resolved,

        [Display(Name = "Closed")]
        Closed
    }
}
