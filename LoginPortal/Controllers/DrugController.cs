using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LoginPortal.MailOrderContext;
using LoginPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoginPortal.Controllers
{
    public class DrugController : Controller
    {
        Uri baseAddress = new Uri("http://20.193.144.163/api");    //https://localhost:44372
        HttpClient client;
        PharmacyContext context;
        public DrugController(PharmacyContext _con)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = _con;

        }
        /// <summary>
        /// This Method us giving the View For Checking Drug details
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Check_DrugByID(int drugid)
        {
            string Token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            
            HttpResponseMessage response = client.GetAsync(client.BaseAddress+ "/Drugs/GetDrugDetails/" + drugid).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                Drug drug = JsonConvert.DeserializeObject<Drug>(scheduleData);
                context.DrugDetails.Add(drug);
                context.SaveChanges();

                return View("DrugDetailsid", drug);
            }
            return View();

        }
        [HttpPost]
        public IActionResult DrugDetailsid(Drug obj)
        {
            return View();
        }

        public ActionResult Check_DrugByName(Drug obj)
        {
            string drugname = obj.Name;
            string Token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Drugs/GetDrugDetailByName/" + drugname).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                Drug drug = JsonConvert.DeserializeObject<Drug>(scheduleData);
                context.DrugDetails.Add(drug);
                context.SaveChanges();

                return View("DrugDetailsname", drug);
            }
            return View();

        }
        [HttpPost]
        public IActionResult DrugDetailsname(Drug obj)
        {
            return View();
        }

        public ActionResult GetByAll(int id, string loc)
        {
            string Token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Drugs/" + id+"/"+loc).Result;
            if (response.IsSuccessStatusCode)
            {
                string scheduleData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Drugloc> drug = JsonConvert.DeserializeObject<List<Drugloc>>(scheduleData);
                return View("DrugDetailsall", drug);
            }
            return View();

        }
        [HttpPost]
        public IActionResult DrugDetailsall(Drugloc obj)
        {
            return View();
        }
    }
}
