using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        [HttpPost]
        public ActionResult UserPicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var pathname = Path.Combine(Server.MapPath("~/Content/Images"), filename); //Saves the picture in Images
                file.SaveAs(pathname);


                PictureRepository.UploadPicture(User.Identity.Name, filename);


            }
            return RedirectToAction("Profile", "User", new { username = User.Identity.Name });
        }
    }
}