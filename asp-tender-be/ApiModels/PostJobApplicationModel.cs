using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace asp_tender_be.ApiModels
{
    public class PostJobApplicationModel
    {
        [Required]
        public int PositionID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public IFormFile Cv { get; set; }
    }
}
