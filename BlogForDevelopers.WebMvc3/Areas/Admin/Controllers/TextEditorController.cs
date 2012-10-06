using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogForDevelopers.WebMvc3.Areas.Admin.Controllers
{
	[Authorize]
    public class TextEditorController : Controller
    {
		/// <summary>
		/// GET: Admin/TextEditor/ImageUpload
		/// </summary>
		/// <returns>PartilView: _ImageUpload</returns>
		[HttpGet]
		public PartialViewResult ImageUpload()
		{
			return PartialView("_ImageUpload");
		}

		/// <summary>
		/// POST: /TextEditor/ImageUpload
		/// </summary>
		/// <param name="imageUpload">File for upload.</param>
		/// <returns>Json: {url}</returns>
		[HttpPost]
		public JsonResult ImageUpload(HttpPostedFileBase imageUpload)
		{
			string imageName = FileUpload(imageUpload, Server.MapPath(ConfigurationManager.AppSettings["ResourceImageInServer"]));

			return Json(new { url = ConfigurationManager.AppSettings["ResourceImageInWebSite"] + imageName }, "text/html", JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Upload a file.
		/// </summary>
		/// <param name="fileUpload">File for upload.</param>
		/// <param name="path">Path where the image is saved.</param>
		/// <returns>The file name.</returns>
		private string FileUpload(HttpPostedFileBase fileUpload, string path)
		{
			string nameFile = string.Format("{0}_{1}{2}",
													 DateTime.UtcNow.ToString("MM-dd-yyyy_HH-mm-ss"),
													 Path.GetFileNameWithoutExtension(fileUpload.FileName),
													 Path.GetExtension(fileUpload.FileName)
													 );

			fileUpload.SaveAs(string.Format("{0}/{1}", path, nameFile));
			return nameFile;
		}

    }
}
