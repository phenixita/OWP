using System.ComponentModel.DataAnnotations;

namespace owp_web.Models
{
    public class Worker
    {
        [Key]
        public long WorkerId { get; set; }
        public string FullName { get; set; }
    }
}
