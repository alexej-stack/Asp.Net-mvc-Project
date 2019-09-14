using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.Models;
using System.IO;

namespace PhotoGalleryDemo.Controllers
{
    public class ManageAlbumsController : Controller
    {
        AlbumModelService objAlbumModelService = new AlbumModelService();

        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "None")]
        public ActionResult Index()
        {
            ViewBag.total = objAlbumModelService.GetAlbums().ToList().Count;
            return View(objAlbumModelService.GetAlbums().ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            AlbumMaster objAlbumMaster = new AlbumMaster();
            objAlbumMaster.ImageName = collection["ImageName"].ToString();
            objAlbumMaster.Image = ConvertToBytes(file);
            objAlbumModelService.UploadAlbums(objAlbumMaster);
            return View(objAlbumModelService.GetAlbums().ToList());
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = objAlbumModelService.GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}
