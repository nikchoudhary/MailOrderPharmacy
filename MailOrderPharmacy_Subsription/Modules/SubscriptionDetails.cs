using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacySubscription.Modules
{
    public class SubscriptionDetails
    {
        public int Drug_ID { get; set; }
        public string Drug_name { get; set; }
        public int Sub_id { get; set; }
        public string Member_Location { get; set; }
        public int MemberID { get; set; } 
        public DateTime SubscriptionDate { get; set; } 
        public int PrescriptionID { get; set; }
       public string RefillOccurrence{get ; set;} 
        public bool SubscriptionStatus { get; set; }
    }
}
