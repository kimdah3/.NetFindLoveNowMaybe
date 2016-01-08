using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SearchRepository : IDisposable
    {
        public FindLoveDbEntities Context { get; set; }

        public SearchRepository()
        {
            Context = new FindLoveDbEntities();
        }

        public List<User> SearchUserByUserName(string searchUserName)
        {
            var foundUsers = new List<User>();

            var searchUsers = from user in Context.User
                              where user.UserName.StartsWith(searchUserName.Trim())
                                && user.Visible
                              select user;
            foreach (var user in searchUsers)
            {
                foundUsers.Add(user);
            }

            return foundUsers;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
