using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoginPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoginPortal.Controllers
{
    public class MemberController : Controller
    {
        
        IConfiguration _config;
        public MemberController(IConfiguration config)
        {
            
            _config = config;
        }
        /// <summary>
        /// This method Returns the Login Page View
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            HttpContext.Response.Cookies.Delete("Token");
            return View();

        }
        /// <summary>
        /// This method creates the View for Authorized Users
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Member obj)
        {
            string TokenForLogin;
            string data = JsonConvert.SerializeObject(obj);
            
            try
            {
                TokenForLogin = GetToken("http://20.193.136.176/api/Auth/", obj);
                
                if (!string.IsNullOrEmpty(TokenForLogin))
                {
                    HttpContext.Response.Cookies.Append("Token", TokenForLogin);
                    return View("Index");
                }
                ViewBag.Message = "Invalid ID or Password";
                return View("Login");
            }
            catch (Exception ex)
            {
                return View("Error1", ex);
            }
        }
        
        public ActionResult Index()
        {
            
            string Token = HttpContext.Request.Cookies["Token"];
            if(string.IsNullOrEmpty(Token))
            {
                return View("Login");
            }
            return View();
        }
        
            

        /// <summary>
        /// This method generates Token for Authorized Users
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        static string GetToken(string url, Member user)
        {

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                if(response.IsSuccessStatusCode)
                {
                    string token = response.Content.ReadAsStringAsync().Result;
                    return token;
                }
               
                return null;
            }
        }
    }
}
