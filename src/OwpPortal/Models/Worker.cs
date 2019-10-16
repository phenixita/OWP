using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace owp_web.Models
{
    [NotMapped]
    public class Worker
    {
        public string AssignmentId { get; set; }
        public string PrincipalDisplayName { get; set; }
        public Guid? PrincipalId { get; set; }
    }
}
