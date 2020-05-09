using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace asp_tender_be.Models
{
    public class JobApplicationAnswer
    {
        public int ID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        public bool Accepted { get; set; }

        public virtual JobApplication JobApplication { get; set; }
    }
}
