using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FS20.Models
{
    public class PlayerMembershipFee
    {
        [Key]
        public int PlayerMembershipFeeID { get; set; }
        [Required(ErrorMessage = "Type in please")]
        [StringLength(50)]
        public int Year { get; set; }
        public int Month { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }

        public int MembershipFeeID { get; set; }
        public MembershipFee MembershipFee { get; set; }
    }
}
