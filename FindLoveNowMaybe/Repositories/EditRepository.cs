using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EditRepository
    {
        
        public void UpdatePerson(string currentUser, User person)
        {
            using (var FindLoveDb = new FindLoveDbEntities())
            {
                User currentU = FindLoveDb.User.FirstOrDefault(x => x.UserName == currentUser);

                currentU.FirstName = person.FirstName;
                currentU.LastName = person.LastName;
                currentU.UserName = person.UserName;
                currentU.Password = person.Password;
                currentU.Description = person.Description;
                currentU.Age = person.Age;
                currentU.Sex = person.Sex;
                currentU.InterestedMen = person.InterestedMen;
                currentU.InterestedWomen = person.InterestedWomen;
                currentU.Active = person.Active;
                currentU.Visible = person.Visible;

                FindLoveDb.SaveChanges();
            }
        }


    }
}