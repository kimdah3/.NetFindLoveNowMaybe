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

        public void SaveUser()
        {
                Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
