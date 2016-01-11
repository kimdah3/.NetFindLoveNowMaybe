using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository
    {
        public List<Category> ReturnAllFriendsWithCategory(string userName)
        {
            int userId;
            var returnList = new List<Category>();
            using (var userRepo = new UserRepository())
            {
                userId = userRepo.GetUserByUserName(userName).Id;
            }
            using (var db = new FindLoveDbEntities())
            {
                var result = from r in db.Category
                             where r.ActiveUserId == userId
                             select r;

                foreach (var item in result)
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        /* Method returns true if friend is already categorised. */
        public bool IsFriendCategorised(string ActiveUserUsername, string FriendUserName)
        {
            int ActiveUserId;
            int FriendId;
            using (var userRep = new UserRepository())
            {
                ActiveUserId = userRep.GetUserByUserName(ActiveUserUsername).Id;
                FriendId = userRep.GetUserByUserName(FriendUserName).Id;
            }
            using (var db = new FindLoveDbEntities())
            {
                var p = db.Category.FirstOrDefault(x => x.ActiveUserId == ActiveUserId && x.FriendId == FriendId);
                return p == null;
            }
        }

        public void AddCategoryToFriend(string activeUserName, string friendUserName, string category)
        {
            int activeUserId;
            int friendId;

            using (var userRep = new UserRepository())
            {
                activeUserId = userRep.GetUserByUserName(activeUserName).Id;
                friendId = userRep.GetUserByUserName(friendUserName).Id;
            }

            using (var db = new FindLoveDbEntities())
            {
                var friendCategory = new Category();
                friendCategory.ActiveUserId = activeUserId;
                friendCategory.FriendId = friendId;
                friendCategory.Category1 = category;
                db.Category.Add(friendCategory);
                db.SaveChanges();
            }
        }

        public List<User> GetFriendsByCategory(string userName, string categoryName)
        {
            var users = new List<User>();
            var categories = new List<Category>();
            int userId;
            using (var userRepo = new UserRepository())
            {
                userId = userRepo.GetUserByUserName(userName).Id;

                using (var db = new FindLoveDbEntities())
                {
                    categories = ReturnAllFriendsWithCategory(userName);
                    foreach (var category in categories)
                    {
                        if (string.Equals(category.Category1, categoryName, StringComparison.OrdinalIgnoreCase))
                        {
                            var user = userRepo.GetUserById(category.FriendId);
                            if (user.Active == true)
                            {
                                users.Add(user);
                            }
                        }
                    }
                }

            }

            return users;
        }


        public List<string> GetAllCategoriesForUser(string userName)
        {
            int userId;
            var returnList = new List<string>();

            using (var userRep = new UserRepository())
            {
                userId = userRep.GetUserByUserName(userName).Id;
            }

            using (var db = new FindLoveDbEntities())
            {
                returnList = (from r in db.Category
                              where r.ActiveUserId == userId
                              select r.Category1).Distinct().ToList();
            }

            return returnList;
        }


    }
}
