using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace asp_tender_be.Models
{
    public class Position
    {
        public int ID { get; set; }

        [Required]
        public int WorkplaceID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Workplace Workplace { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
