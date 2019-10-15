using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace owp_web.Models
{
    public class WorkItem
    {
        [Key]
        public long WorkItemId { get; set; }

        public string Description { get; set; }

        public WorkItemTypes WorkItemType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastChangedOn { get; set; }

        public WorkItemStatus Status { get; set; }

        public Worker AssignedTo { get; set; }
    }
}
