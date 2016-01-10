using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;

namespace FindLoveNowMaybe.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Search(string searchText)
        {
            var model = new SearchModel();

            using (var searchRepository = new SearchRepository())
            {
                model.FoundUsers = searchRepository.SearchUserByUserName(searchText);
            }
            model.SearchText = "";

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            var searchtext = model.SearchText;
            var searchResultModel = new SearchModel();

            using (var searchRepository = new SearchRepository())
            {
                searchResultModel.FoundUsers = searchRepository.SearchUserByUserName(searchtext);
            }

            return View(searchResultModel);
        }
    }
}