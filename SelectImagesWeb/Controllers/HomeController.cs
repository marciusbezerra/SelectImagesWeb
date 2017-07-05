using SelectImagesWeb.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SelectImagesWeb.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var path = HttpContext.Server.MapPath(@"~/Images");
			var images = new List<string>(Directory.GetFiles(path, "*.jp*g"));
			images.AddRange(Directory.GetFiles(path, "*.gif"));
			images.AddRange(Directory.GetFiles(path, "*.mp4"));
			var imageCount = images.Count;
			var firstImage = images.OrderBy(s => s).FirstOrDefault();
			var folders = Directory.GetDirectories(path);

			var selectModel = new SelectModel
			{
				CurrentImage = Path.GetFileName(firstImage),
				MoveToFolder = null,
				Folders = new List<string>(folders.Select(Path.GetFileName)),
				ImageCount = imageCount
			};

			return View(selectModel);
		}

		public ActionResult Folder(string folderName)
		{
			var path = HttpContext.Server.MapPath(@"~/Images");
			var folder = Path.Combine(path, folderName);
			var imagesInfolder = new List<string>();
			if (Directory.Exists(folder))
			{
				imagesInfolder = Directory.GetFiles(folder, "*.jp*g").Select(f => Path.GetFileName(f)).ToList();
			}
			return View("FolderFiles", imagesInfolder);
		}

		public ActionResult Move(SelectModel selectModel)
		{
			var filename = HttpContext.Server.MapPath("~/Images/" + selectModel.CurrentImage);
			if (filename != null && selectModel.MoveToFolder != null && System.IO.File.Exists(filename))
			{
				var imageDirectory = Path.GetDirectoryName(filename);
				var moveToFolder = Path.Combine(imageDirectory, selectModel.MoveToFolder);
				var newFilename = Path.Combine(moveToFolder, Path.GetFileName(filename));
				if (!Directory.Exists(moveToFolder))
					Directory.CreateDirectory(moveToFolder);
				System.IO.File.Move(filename, newFilename);
			}
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}