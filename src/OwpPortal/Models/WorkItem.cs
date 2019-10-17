using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using owp_web.Helpers;

namespace owp_web.Models
{
    public class WorkItem
    {
        [Display(Name = "Tracking number")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WorkItemId { get; set; }

        [Display(Name = "Description")]
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

        [NotMapped]
        [Display(Name = "Assigned To")]
        public Worker AssignedTo
        {
            get
            {
                return (new GraphAPI()).GetWorkerByAssignmentIdAsync(AssignmentId).Result;
            }
            set
            {
                AssignmentId = value.AssignmentId;
            }
        }

        public string AssignmentId { get; set; }

        [Display(Name = "Address of issue")]
        public string Address { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? @Longitude { get; set; }

        public string StatusName
        {
            get
            {
                return this.Status.ToName();
            }
        }

        public string TypeName
        {
            get
            {
                return this.WorkItemType.ToName();
            }
        }

        [Display(Name = "Image of issue")]
        public byte[] Image { get; set; }

        [Display(Name = "Priority")]
        public WorkItemPriority? WorkItemPriority { get; set; }
    }
}
