using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MatchingRepository
    {
        public string MatchUsers(string activeUserName, string matchUserName)
        {
            User activeUser;
            User matchUser;
            int matchValue;

            using (var userRep = new UserRepository())
            {
                activeUser = userRep.GetUserByUserName(activeUserName);
                matchUser = userRep.GetUserByUserName(matchUserName);

            }

            if (!matchUser.InterestedWomen && !activeUser.Sex || !matchUser.InterestedMen && activeUser.Sex || !activeUser.InterestedMen && matchUser.Sex || !activeUser.InterestedWomen && !matchUser.Sex)
            {
                matchValue = 5;
            }
            else
            {
                matchValue = 20;
                int ageSpanMin = (int.Parse(activeUser.Age) - 5);
                int ageSpanMax = (int.Parse(activeUser.Age) + 5);

                if (matchUser.Age == activeUser.Age)
                {
                    matchValue += 80;
                }

                else if (int.Parse(matchUser.Age) > ageSpanMin && int.Parse(matchUser.Age) < ageSpanMax)
                {
                    matchValue += 50;
                }


                else if (int.Parse(matchUser.Age) > (ageSpanMin - 5) && int.Parse(matchUser.Age) < (ageSpanMax + 5))
                {
                    matchValue += 30;
                }

            }
            if (matchValue <= 70)
            {
                var random = new Random().Next(30);
                matchValue = matchValue + random;
            }

            return matchValue + "";
        }
    }
}