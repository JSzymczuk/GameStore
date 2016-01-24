using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using GameStore.Helpers;
using System.Net.Mail;
using System.Net;

namespace GameStore.Controllers
{
    public class NotificationController : Controller
    {
        private ApplicationUserManager _userManager;

        public NotificationController() { }

        public NotificationController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new NotificationViewModel());
        }

        [HttpPost]
        public ActionResult Index(NotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var myID = AccountHelper.GetLoggedUserId();
                var users = UserManager.Users.ToList();

                foreach (var user in users)
                {
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("2016gamestore@gmail.com", "GameStore123"),
                        EnableSsl = true
                    };
                    client.Send("2016gamestore@gmail.com", user.Email, model.Title, model.Body);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public ActionResult AddToNewsletter(string email)
        {
            if (email != "")
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("2016gamestore@gmail.com", "GameStore123"),
                    EnableSsl = true
                };
                client.Send("2016gamestore@gmail.com", "2016gamestore@gmail.com", "Nowa osoba do newslettera", 
                    "Proszę mnie informować o tym, co nowego dzieje się w świecie gier na maila " + email + ". Z góry dziękuję.");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}