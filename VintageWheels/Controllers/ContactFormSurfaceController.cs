using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;
using VintageWheels.Models;

namespace VintageWheels.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        /// <summary>
        /// Renders the Contact Form
        /// @Html.Action("RenderContactForm","ContactFormSurface");
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderContactForm()
        {
            //Return a partial view ContactForm.cshtml in /views/ContactFormSurface/ContactForm.cshtml
            //With an empty/new ContactFormViewModel
            return PartialView("ContactForm", new ContactFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleContactForm(ContactFormViewModel model)
        {
            //Check if hidden field was filled in 
            if (!string.IsNullOrEmpty(model.Tip))
            {
                return CurrentUmbracoPage();
            }

            //Check if captcha was good
            var googleToken = Request["g-recaptcha-response"];
            bool validCaptcha = ValidateCaptcha(googleToken);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated
                return CurrentUmbracoPage();
            }

            LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "validCaptcha is "+validCaptcha.ToString());
            if (!validCaptcha)
            {
                return CurrentUmbracoPage();
            }
            
            using (SmtpClient smtpClient = new SmtpClient("127.0.0.1", 25))
            {

                smtpClient.Credentials = new System.Net.NetworkCredential("Mailer@vintage-wheels.be", "M@1lerJ3113M@art3n");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = false;

                MailMessage email = new MailMessage
                {
                    Body = "<p>from: " + model.Name + "</p>" + "<p>email: " + model.Email + "</p>" + "<p>Subject: " + model.Subject + "</p>" + "<p>Message: " + model.Message + "</p>",
                    IsBodyHtml = true,
                    Subject = model.Subject != null ? model.Subject : "Contact request"
                };
                //Setting From , To and CC
                email.From = new MailAddress("site@vintage-wheels.be");
                email.To.Add(new MailAddress("info@vintage-wheels.be"));


                try
                {
                    //Try & send the email with the SMTP settings
                    LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "mail send is true");

                    smtpClient.Send(email);
                }
                catch (Exception ex)
                {
                    //Throw an exception if there is a problem sending the email
                    LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "mail send failed", ex);

                    throw ex;
                }
            }

            //Update success flag (in a TempData key)
            //TempData["IsSuccessful"] = true;

            //All done - lets redirect to the current page & show our thanks/success message
            return RedirectToCurrentUmbracoPage();
        }

        private bool ValidateCaptcha(string googleToken)
        {
            // new { secret = "", response = googleToken }
            HttpClient client = new HttpClient();
            var secret = "6Le541EUAAAAAG0jUZp9Zb8d2IkjXkQeRAEB91_N";

            //LogErrorsAndTransaction("secret: " + secret + " | token: " + googleToken);
            client.BaseAddress = new Uri("https://www.google.com/recaptcha/api/");
            //Secret key gebruikt 

            var body = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("secret", secret),
                new KeyValuePair<string, string>("response", googleToken)
            };
            var reply = client.PostAsync("siteverify", new FormUrlEncodedContent(body)).Result.Content.ReadAsStringAsync().Result;
            if (reply.ToLower().Contains("false"))
            {
                //LogErrorsAndTransaction(reply);
                LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "captcha is false", new Exception("captcha false"));
                return false;

            }
            else
            {
                //LogErrorsAndTransaction(reply);
                LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "captcha is true");
                return true;
            }

        }
    }
}