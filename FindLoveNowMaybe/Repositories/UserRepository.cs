using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IDisposable
    {
        public FindLoveDbEntities Context { get; set; }
        public UserRepository()
        {
            this.Context = new FindLoveDbEntities();
        }

        public User GetFirstUser()
        {
                return Context.User.FirstOrDefault();
        }

        public User GetUser(int id)
        {
                return Context.User.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
                Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public User LoginUser(string userName, string password)
        {
            var user = Context.User.FirstOrDefault(x => x.UserName == userName && x.Password.Equals(password));
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            return Context.User.FirstOrDefault(x => x.UserName.Equals(userName));
        }
    }
}
