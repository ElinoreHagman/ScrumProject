﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumProject.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Resources;
using System.Net;

namespace ScrumProject.Controllers
{
    public class EmailNotifyController : Controller
    {
         

        /// Get: /EmailNotify/Index method.  
        /// Return index view
        public ActionResult Index()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return this.View();
        }

        

        /// <summary>  
        /// POST: /EmailNotify/Index  
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <returns>Return - Response information</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailNotifyViewModel model)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Initialization  
                    string emailMsg = "Dear " + model.ToEmail + ", <br /><br /> A post has been made in one of the categories you subscribe to!<b style='color: blue'> ORU Blog Notification </b> <br /><br /> Thanks & Regards, <br />ORU Bloggsters";
                    string emailSubject = EmailInfo.EMAIL_SUBJECT_DEFAULT + " Blog Notification";

                    // Sends Email  
                    await this.SendEmailAsync(model.ToEmail, emailMsg, emailSubject);


                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return View();
             
        }

        

      

        /// <summary>  
        ///  Send Email method.  
        /// </summary>  
        /// <param name="email">Email parameter</param>  
        /// <param name="msg">Message parameter</param>  
        /// <param name="subject">Subject parameter</param>  
        /// <returns>Return await task</returns>  
        public async Task<bool> SendEmailAsync(string email, string msg, string subject = "")
        {
            // Initialization.  
            bool isSend = false;

            try
            {
                // Initialization.  
                var body = msg;
                var message = new MailMessage();

                // Settings.  
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(EmailInfo.FROM_EMAIL_ACCOUNT);
                message.Subject = !string.IsNullOrEmpty(subject) ? subject : EmailInfo.EMAIL_SUBJECT_DEFAULT;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = EmailInfo.FROM_EMAIL_ACCOUNT,
                        Password = EmailInfo.FROM_EMAIL_PASSWORD
                    };

                    smtp.Credentials = credential;
                    smtp.Host = EmailInfo.SMTP_HOST_GMAIL;
                    smtp.Port = Convert.ToInt32(EmailInfo.SMTP_PORT_GMAIL);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(message);

                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // info.  
            return isSend;
        }
        //public ActionResult NewPostNotify ()

      
    }
}