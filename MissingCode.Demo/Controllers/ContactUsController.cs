using MissingCode.Demo.Models;
using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace MissingCode.Demo.Controllers
{
    public class ContactUsController : SurfaceController
    {
        [HttpPost]
        public ActionResult HandleForm(ContactUsModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("~/Views/Partials/ContactUsForm.cshtml");

            var config = Umbraco.ContentAtRoot().First();

            var body = "<table width=\"700\" border=\"0\" align=\"center\" cellpadding=\"10\" cellspacing=\"10\" style=\"background:#57574a;\">" +
                $"<tr><th bgcolor=\"#f7f7f7\">Date:</th><td bgcolor=\"#f7f7f7\">{DateTime.Now.ToString("dd-MMM-yyyy HH:mm")}</td></tr>" +
                $"<tr><th bgcolor=\"#f7f7f7\">Name:</th><td bgcolor=\"#f7f7f7\">{model.Name}</td></tr>" +
                $"<tr><th bgcolor=\"#f7f7f7\">Email:</th><td bgcolor=\"#f7f7f7\">{model.Email}</td></tr>" +
                $"<tr><th bgcolor=\"#f7f7f7\">Phone:</th><td bgcolor=\"#f7f7f7\">{model.Phone}</td></tr>" +
                "</table>";
            var message = new MailMessage()
            {
                Subject = config.Value<string>("mailSubject"),
                IsBodyHtml = true,
                Body = body,

            };
            message.To.Add(config.Value<string>("mailTo"));
            message.IsBodyHtml = true;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            var client = new SmtpClient();

            client.Send(message);


            return PartialView("~/Views/Partials/ContactUsFormSent.cshtml");
        }
    }
}