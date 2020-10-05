using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPortal.Models
{
    public class Subscription
    {
        [Required(ErrorMessage ="Enter Valid Drug ID")]
        public int Drug_ID { get; set; }
        [Required]
        public int Sub_id { get; set; }
        public string Drug_Name { get; set; }
        [Required]
        public string Member_Location { get; set; }
        [Required]
        public int MemberID { get; set; }
        [Required]
        public DateTime SubscriptionDate { get; set; }
        [Required]
        public int PrescriptionID { get; set; }
        [Required(ErrorMessage ="Refill Ocurance Should Be Weekly or Monthly")]
        public string RefillOccurrence { get; set; }
        [Required]
        public bool SubscriptionStatus { get; set; }
    }
}
