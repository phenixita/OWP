using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace owp_web.Models
{
    public class WorkItemViewModel
    {
        #region Public Properties
        public WorkItem WorkItem { get; set; }
     
        public List<SelectListItem> WorkItemStatuses
        {
            get
            {
                var listItems = new List<SelectListItem>();
                var items = Enum.GetValues(typeof(WorkItemStatus));

                foreach (var item in items)
                {
                    listItems.Add(new SelectListItem(item.ToString(), item.ToString()));
                }
                return listItems;
            }
        }
        public List<SelectListItem> WorkItemTypes
        {
            get
            {
                var listItems = new List<SelectListItem>();
                var items = Enum.GetValues(typeof(WorkItemTypes)).Cast<WorkItemTypes>();
                foreach(var item in items)
                {
                    listItems.Add(new SelectListItem(item.ToString(), item.ToString()));
                }
                return listItems;
            }
        }
        #endregion

        #region Constructors
        public WorkItemViewModel()
        {
            WorkItem = new WorkItem();
        }

        public WorkItemViewModel(WorkItem w)
        {
            WorkItem = w;
        }
        #endregion
    }
}
