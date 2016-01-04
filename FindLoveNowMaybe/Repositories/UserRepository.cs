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

        public User GetUserById(int id)
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

        public bool IsUniqueUserName(string userName)
        {
            var user = GetUserByUserName(userName);
            return user == null;
        }

        public List<User> GetFourRandomUsers()
        {
            var randomUsers = new List<User>();
            var unOrderedUsers = Context.User.OrderBy(x => Guid.NewGuid()).ToList();
            randomUsers.Add(unOrderedUsers[0]);
            randomUsers.Add(unOrderedUsers[1]);
            randomUsers.Add(unOrderedUsers[2]);
            randomUsers.Add(unOrderedUsers[3]);
            return randomUsers;
        }

    }
}
