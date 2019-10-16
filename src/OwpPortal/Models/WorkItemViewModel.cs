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
