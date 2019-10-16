using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using owp_web.Helpers;

namespace owp_web.Models
{
    public class WorkItemViewModel
    {
        private GraphAPI _api;

        #region Public Properties
        public WorkItem WorkItem { get; set; }
        #endregion

        #region Constructors
        public WorkItemViewModel()
        {
            _api = new GraphAPI();
            WorkItem = new WorkItem();
        }

        public WorkItemViewModel(WorkItem w)
        {
            _api = new GraphAPI();
            WorkItem = w;
        }

        public async Task<List<Worker>> GetWorkersListAsync()
        {
            return await _api.GetWorkersListAsync();
        }
        #endregion
    }
}
