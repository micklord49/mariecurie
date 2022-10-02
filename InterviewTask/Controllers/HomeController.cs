using InterviewTask.Models;
using InterviewTask.Services;
using System.IO;
using System;
using System.Web.Mvc;
using WebGrease.Activities;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */
        public ActionResult Index()
        {
            FileLogger log = new FileLogger(Server.MapPath("~/log.txt"));
            IndexModel model = null;
            try
            {
                HelperServiceRepository repository = new HelperServiceRepository();
                model = new IndexModel(repository);

                log.Log($"ACCESS: From {GetIPAddress()}");
            }
            catch (Exception ex)
            {
                log.Log($"EXCEPTION: {ex.Message}");
            }

            return View(model);
        }


        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        
        
    }
}