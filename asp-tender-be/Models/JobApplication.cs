using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace asp_tender_be.Models
{
    public class JobApplication
    {
        public int ID { get; set; }

        [Required]
        public int PositionID { get; set; }

        public int? JobApplicationAnswerID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public byte[] CvData { get; set; }

        [Required]
        public string CvMimeType { get; set; }

        [Required]
        public string CvFileName { get; set; }

        public virtual Position Position { get; set; }
        public virtual JobApplicationAnswer JobApplicationAnswer { get; set; }
    }
}
