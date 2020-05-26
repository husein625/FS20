using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS20.Models
{
    public class TrainingType
    {
        [Key]
        public int TrainingTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
