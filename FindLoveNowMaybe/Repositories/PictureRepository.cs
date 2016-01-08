using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PictureRepository
    {
        public static void UploadPicture(string username, string picture)
        {
            using (var db = new FindLoveDbEntities())
            {
                User userPicture = db.User.FirstOrDefault(x => x.UserName == username);
                userPicture.Picture = picture;
                db.SaveChanges();
            }
        }
    }
}