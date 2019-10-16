using System.ComponentModel.DataAnnotations;

namespace owp_web.Models
{
    public enum WorkItemStatus
    {

        [Display(Name = "New")]
        New,

        [Display(Name = "Assigned")]
        Assigned,

        [Display(Name = "Re-Assigned")]
        Resolved,

        [Display(Name = "Done")]
        Done
    }
}
