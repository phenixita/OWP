using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace owp_web.Models
{
    public class WorkItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WorkItemId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Display(Name = "Work Item Type")]
        public WorkItemTypes WorkItemType { get; set; }

        [Display(Name = "Created On")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Last Changed On")]
        public DateTime LastChangedOn { get; set; }

        public WorkItemStatus Status { get; set; }

        [Display(Name = "Assigned To")]
        public Worker AssignedTo { get; set; }

        [Display(Name = "Address of issue")]
        public string Address { get; set; }
    }
}
