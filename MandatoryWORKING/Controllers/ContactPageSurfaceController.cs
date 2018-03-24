using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using umbracoInitializer.ViewModels;

namespace umbracoInitializer.Controllers
{
    public class ContactPageSurfaceController : SurfaceController
    {
        // GET: ContactPageSurface
        public ActionResult Index()
        {
            return PartialView("ContactPagePartial", new ContactForm());
        }

        [HttpPost]
        public ActionResult SubmitForm(ContactForm contactForm) {
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }


            //instansiating mailmessage object with posted values
            MailMessage mail = new MailMessage()
            {
                Subject = contactForm.Subject,
                From = new MailAddress(contactForm.Email, contactForm.Name),
                Body = contactForm.Message
            };

            mail.To.Add("eaarhl@eaaa.dk");

            //places mail on c driv under c:/TEMP/mail
            try { 
            using(SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = "C:\\TEMP\\Mail";
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = false;
              //  smtp.Host = "smtp.gmail.com";
               // smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("rhl@miracle.dk", "1234");

                smtp.Send(mail);
            }
            }catch(Exception e)
            {
                //You would here log the error and return a custom error message to the client, if an exception was thrown
            }

            //create the message node in the backoffice with the subject as 
            //first instansiate the node object and set values
            IContent message = Services.ContentService.CreateContent(contactForm.Subject, CurrentPage.Id,"message");

            message.SetValue("messageName", contactForm.Name);
            message.SetValue("email", contactForm.Email);
            message.SetValue("messageContent", contactForm.Message);
            message.SetValue("subject", contactForm.Subject);

            //save the node
            Services.ContentService.Save(message);

            //If process 
            TempData["Success"] = true;
            return CurrentUmbracoPage();
        }
    }
}