using EjemploVideos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjemploVideos.Controllers
{
    public class VideosController : Controller
    {
        // GET: Videos
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveVideos(HttpPostedFileBase fileUpload)
        {
            try
            {
                //fileUpload
                using (var db = new EjemploVideos.Models.EjemploBlobEntities())
                {
                    MemoryStream target = new MemoryStream();
                    fileUpload.InputStream.CopyTo(target);
                    Videos video = new Videos() { id = Guid.NewGuid(), data = target.ToArray() };
                    db.Videos.Add(video);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Value = true, Message = "Subido con éxito a BBDD" }, JsonRequestBehavior.AllowGet);
        }
    }
}