using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Controllers
{
	public class ContactController : Controller
	{
		//
		// GET: /Contact/
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string name, string email, string message)
		{
			string templete =
				@"
					<h2>Name: @Model.Name</h2>
					<h4>Email: @Model.Email</h4>
					<h4>Message</h4>
					<p>@Model.Message</p>
				";

			dynamic model = new
			{
				Name = name,
				Email = email,
				Message = message
			};

			MailAddress fromAddress = new MailAddress(
				ConfigurationManager.AppSettings["SmtpUserEmail"],
				ConfigurationManager.AppSettings["SmtpUserName"]);

			MailAddress toAddress = new MailAddress(
				ConfigurationManager.AppSettings["SendEmailToAddress"],
				ConfigurationManager.AppSettings["SendEmailToName"]);

			MailMessage mailMensagem = new MailMessage(fromAddress, toAddress);
			mailMensagem.Subject = ConfigurationManager.AppSettings["SubjectEmail"];
			mailMensagem.Body = RazorEngine.Razor.Parse(templete, model);
			mailMensagem.IsBodyHtml = true;
			mailMensagem.BodyEncoding = UTF8Encoding.UTF8;
			mailMensagem.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

			SmtpClient smtpClient = new SmtpClient(
				ConfigurationManager.AppSettings["SmtpHost"],
				Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]));

			smtpClient.Credentials = new NetworkCredential(
				ConfigurationManager.AppSettings["SmtpUserEmail"],
				ConfigurationManager.AppSettings["SmtpPassword"]);

			try
			{
				smtpClient.EnableSsl = true;
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.Send(mailMensagem);
				ViewBag.Sucess = true;
			}
			catch (Exception)
			{
				ViewBag.Sucess = false;

				throw;
			}

			return View();
		}

	}
}
