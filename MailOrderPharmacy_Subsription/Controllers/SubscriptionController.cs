using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MailOrderPharmacySubscription.Modules;
using MailOrderPharmacySubscription.Repository;
using Microsoft.AspNetCore.Authorization;
//using MailOrderPharmacy_Subsription.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailOrderPharmacySubscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private ISubscriptionRepository _sub;

        public SubscriptionController(ISubscriptionRepository sub)
        {   
            _sub = sub;
        }
        string Token;
        public SubscriptionController()
        {
            Token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
        }
        public static List<SubscriptionDetails> ls = new List<SubscriptionDetails>
        {
            new SubscriptionDetails
            {
                Drug_ID=1,
                Sub_id=7,
                RefillOccurrence="Weekly",
                Member_Location="Pune"
            },
            new SubscriptionDetails
            {
                Drug_ID=2,
                Sub_id=8,
                RefillOccurrence="Monthly",
                Member_Location="Pune"
            }
        };
        public static List<Prescription> pre = new List<Prescription>
        {
            new Prescription
            {
                drugID=1,
                MemberID=1,
                InsuranceProvider="MediBuddy"
                
                
            }

        };
        /// <summary>
        /// This method returns the details using Subscription ID
        /// </summary>
        /// <param name="subid"></param>
        /// <returns></returns>
        [HttpGet("ViewDetails_BySubID/{subid}")]
        public IActionResult ViewDetails_BySubID(int subid)
        {
            var item = _sub.ViewDetailsByID(subid);
            if (item == null)
                return null;

            return Ok(item);
        }


        /// <summary>
        /// This method Allowing subscription by checking drug name
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("AddSubscription")]
        public ActionResult Add_Subscription(SubscriptionDetails obj)
        {
            if (HttpContext == null)
            {
                Token = "Token";
            }
            else
            {
                Token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
            }

            //SubscriptionRepository sub = new SubscriptionRepository();
            string status = _sub.AddSubscription(obj, Token);
            if (status == null)
                return BadRequest();
            return Ok(status);
        }
        /// <summary>
        /// This method Allowing unsubscription by checking refill status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("RemoveSubscription")]
        public ActionResult Remove_Subscription(SubscriptionDetails obj)
        {
            if (HttpContext == null)
            {
                Token = "Token";
            }
            else
            {
                Token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
            }
            //SubscriptionRepository sub = new SubscriptionRepository();
            string status = _sub.RemoveSubscription(obj, Token);
            if (status == null)
                return BadRequest();
            return Ok(status);
            
        }

       
    }
}
