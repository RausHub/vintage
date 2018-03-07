﻿using System;
using System.Net.Mail;
using System.Web.Mvc;
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
            if (!string.IsNullOrEmpty(model.Tip))
            {
                return CurrentUmbracoPage();
            }

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated
                return CurrentUmbracoPage();
            }

            //Generate an email message object to send
            MailMessage email = new MailMessage(model.Email, "info@vintage-wheels.be");            
            email.Subject = string.IsNullOrEmpty(model.Subject) ? "Contact Form Request" : model.Subject;
            email.Body = model.Message + Environment.NewLine + model.Name;

            try
            {
                //Connect to SMTP credentials set in web.config
                SmtpClient smtp = new SmtpClient();

                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //Throw an exception if there is a problem sending the email
                throw ex;
            }

            //Update success flag (in a TempData key)
            TempData["IsSuccessful"] = true;

            //All done - lets redirect to the current page & show our thanks/success message
            return RedirectToCurrentUmbracoPage();
        }
    }
}