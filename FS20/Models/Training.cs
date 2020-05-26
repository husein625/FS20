using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS20.Models
{
    public class Training
    {

        [Key]
        public int TrainingID { get; set; }

        [Required(ErrorMessage = "Please enter the training")]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }

        public int? GroupID { get; set; }
        public Group Group { get; set; }

        public int? TrainingTypeID { get; set; }
        public TrainingType TrainingType { get; set; }
    }
}
