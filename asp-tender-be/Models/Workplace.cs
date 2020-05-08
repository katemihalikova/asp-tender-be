using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace asp_tender_be.Models
{
    public class Workplace
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
