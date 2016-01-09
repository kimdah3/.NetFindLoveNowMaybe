using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repositories;

namespace FindLoveNowMaybe.APIController
{
    public class VisitController : ApiController
    {
        [HttpPost]
        public void UserVisiting(string visitorUserName, string visitedUserName)
        {
            var visitRepository = new VisitRepository();
            visitRepository.SaveVisits(visitorUserName, visitedUserName);
        }

    }
}
