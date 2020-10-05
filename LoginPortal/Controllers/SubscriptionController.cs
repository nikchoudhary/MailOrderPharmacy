using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LoginPortal.MailOrderContext;
using LoginPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoginPortal.Controllers
{
    public class SubscriptionController : Controller
    {
        Uri baseAddress = new Uri("http://20.193.128.185/api");   //https://localhost:44318
        HttpClient client;
        readonly PharmacyContext context;
        public SubscriptionController(PharmacyContext _con)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = _con;

        }
        /// <summary>
        /// This Method us giving the View For Subscription
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Subscription_For_Mailorder(Subscription ad)
        {
            string Token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            string data = JsonConvert.SerializeObject(ad);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Subscription/AddSubscription/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                ViewBag.Message = scheduleData;
              
                return View("Index");
            }
            return View();
        }
        /// <summary>
        /// This Method us giving the View For Unsubscription
        /// </summary>
        /// <returns></returns>
        public IActionResult Index1()
        {
            return View();
        }
        public ActionResult Unsubscription_For_Mailorder(Subscription ad)
        {
            string Token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            string data = JsonConvert.SerializeObject(ad);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Subscription/RemoveSubscription/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                ViewBag.Message = scheduleData;
                
                return View("Index1");
            }
            return View();
        }

    }
}
