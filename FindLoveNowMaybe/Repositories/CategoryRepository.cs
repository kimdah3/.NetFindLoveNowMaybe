using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool CheckIfFriendIsCategorised(string ActiveUserUsername, string FriendUserName)
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
                var p = db.Category.FirstOrDefault(x => x.ActiveUserId == ActiveUserId);
                return p == null;
            }
        }
    }
}
