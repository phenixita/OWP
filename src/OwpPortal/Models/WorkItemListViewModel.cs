using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace owp_web.Models
{
    public class WorkItemListViewModel
    {
        public long WorkItemId { get; set; }
        public string Description { get; set; }
        public WorkItemTypes WorkItemType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastChangedOn { get; set; }
        public WorkItemStatus Status { get; set; }
        public Worker AssignedTo { get; set; }
        public string AssignmentId { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? @Longitude { get; set; }
        public string StatusName { get; set; }
        public string TypeName { get; set; }
        public WorkItemPriority? WorkItemPriority { get; set; }
    }
}
