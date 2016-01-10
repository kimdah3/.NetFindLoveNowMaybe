using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SerializeRepository
    {

        public void SerializeProfile(string userName)
        {
            var user = new User();
            var userSerial = new SerializedUser();

            using (var userRep = new UserRepository())
            {
                user = userRep.GetUserByUserName(userName);
            }

            userSerial.Age = user.Age;
            userSerial.Age = user.Description;
            userSerial.InterestedInMen = user.InterestedMen;
            userSerial.InterestedInWomen = user.InterestedWomen;
            userSerial.Sex = user.Sex;
            userSerial.Username = user.UserName;

            var folder = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var full_path = System.IO.Path.Combine(folder, "XML-Profile");
            var xml = new System.Xml.Serialization.XmlSerializer(userSerial.GetType());

            using (var f = System.IO.File.Open(full_path, System.IO.FileMode.Create))
            {
                xml.Serialize(f, userSerial);
                f.Flush();
                f.Close();
            }
        }
    }

    public class SerializedUser
    {
        public string Username { get; set; }
        public string Age { get; set; }
        public bool Sex { get; set; }
        public bool InterestedInMen { get; set; }
        public bool InterestedInWomen { get; set; }
        public string Description { get; set; }
    }
}