using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VisitRepository
    {
        public void SaveVisits(string VisitorUserName, string VisitedUserName)
        {
            using (var userRepo = new UserRepository())
            {
                var VisitorId = userRepo.GetUserByUserName(VisitorUserName).Id;
                var VisitedId = userRepo.GetUserByUserName(VisitedUserName).Id;
                var date = DateTime.Now;
                using (var db = new FindLoveDbEntities())
                {
                    var visit = new Visitors();
                    visit.VisitedUserId = VisitedId;
                    visit.VisitorUserId = VisitorId;
                    visit.Date = date;
                    db.Visitors.Add(visit);
                    db.SaveChanges();
                }
            }
        }

        public List<User> GetFiveLatestVisitors(string userName)
        {
            var visitors = new List<Visitors>();
            var activeUser = new User();
            var latestUser = new List<User>();

            using (var userRepository = new UserRepository())
            {
                activeUser = userRepository.GetUserByUserName(userName);
            }

            using (var db = new FindLoveDbEntities())
            {
                var result = (from r in db.Visitors
                              where r.VisitedUserId == activeUser.Id
                              orderby r.Date descending
                              select r).Take(5);
                visitors = result.ToList();
            }

            using (var userRepository = new UserRepository())
            {
                foreach (var visitor in visitors)
                {
                    latestUser.Add(userRepository.GetUserById(visitor.VisitorUserId));
                }
            }

            return latestUser;
        }


        //public List<User> GetFiveLatestVisitors(string userName)
        //{
        //    var visitors = new List<User>();
        //    var activeUser = new User();
        //    var visitorIds = new List<Visitors>();

        //    using (var userRepository = new UserRepository())
        //    {
        //        activeUser = userRepository.GetUserByUserName(userName);
        //    }

        //    using (var db = new FindLoveDbEntities())
        //    {
        //        var result = (from r in db.Visitors
        //                      where r.VisitedUserId == activeUser.Id
        //                      orderby r.Date descending
        //                      select r).Take(5);
        //        visitorIds = result.ToList();
        //    }
        //    //using (var db = new FindLoveDbEntities())
        //    //{
        //    //    var user = db.User.FirstOrDefault(x => x.Id == visitorIds[0].VisitorUserId);
        //    //    visitors.Add(user);
        //    //}
        //    visitors.Add(new User());
        //    visitors.Add(new User());
        //    visitors.Add(new User());
        //    visitors.Add(new User());

        //    return visitors;
        //}


        //public List<User> GetFiveLatestVisitors(string userName)
        //{
        //    var returnList = new List<User>();
        //    var userRep = new UserRepository();
        //    var userId = userRep.GetUserByUserName(userName).Id;
        //    using (var db = new FindLoveDbEntities())
        //    {
        //        var result = (from r in db.Visitors
        //                      where r.VisitedUserId == userId
        //                      orderby r.Date descending
        //                      select r).Take(5);
        //        foreach (var visit in result)
        //        {
        //            returnList.Add(userRep.GetUserById(visit.VisitorUserId));
        //        }
        //    }
        //    return returnList;
        //}
    }
}
