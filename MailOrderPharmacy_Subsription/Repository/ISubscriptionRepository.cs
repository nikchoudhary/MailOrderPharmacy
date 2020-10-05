using MailOrderPharmacySubscription.Modules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacySubscription.Repository
{
    public interface ISubscriptionRepository
    {
        public dynamic ViewDetailsByID(int Sub_id);
        public string AddSubscription(SubscriptionDetails obj, string Token);
        public dynamic RemoveSubscription(SubscriptionDetails obj, string Token);
    }
}
