
using MVC.FrontEnd.ViewModels;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Linq;
using System;

namespace jump.work.mvc.Controllers
{
    public class SearchController : Controller
    {
        protected static List<AgeRangeVM> AgeRanges = new List<AgeRangeVM>()
        {
            new AgeRangeVM(){ ID = 0 },
            new AgeRangeVM(){ ID = 1, MaxAge = 18},
            new AgeRangeVM(){ ID = 2, MinAge = 18, MaxAge = 21},
            new AgeRangeVM(){ ID = 3, MinAge = 22, MaxAge = 25},
            new AgeRangeVM(){ ID = 4, MinAge = 26, MaxAge = 30},
            new AgeRangeVM(){ ID = 5, MinAge = 30}
        };
        
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new SearchQueryVM() {
                AllAgeRanges = AgeRanges,
                AllSkills = GetSkillsFromDB(), 
                RequiredSkillIDs = ""
            });
        } 

        [HttpGet]
        public ActionResult SearchForPeople(SearchQueryVM search)
        {
            //prepare data
            AgeRangeVM selectedAgeRange = AgeRanges.Where(ageRange => ageRange.ID == search.SelectedAgeRange).FirstOrDefault();

            IEnumerable<int> requiredSkillIDs = new List<int>();
            if (search.RequiredSkillIDs != null && search.RequiredSkillIDs.Length > 0)
            {
                requiredSkillIDs = search.RequiredSkillIDs.Split(',').Select(strID => Convert.ToInt32(strID));
            }

            //Call search method
            IEnumerable<Model.Person> searchResults = Search.SearchHelper.searchForPeople(requiredSkillIDs, search.LikeName, selectedAgeRange.MinAge, selectedAgeRange.MaxAge, search.YearsExperience);

            //Return results to search results view.
            return View("SearchResults", searchResults.ToList());
        }

        [HttpGet]
        public ActionResult GetSkills()
        {
            return Json(GetSkillsFromDB(), JsonRequestBehavior.AllowGet);
        }

        protected IEnumerable<SkillVM> GetSkillsFromDB()
        {
            using (Model.JumpDBContext db = new Model.JumpDBContext())
            {
                return db.Skills.Select(skill => new SkillVM()
                {
                    ID = skill.ID,
                    Name = skill.Name
                }).ToList();
            }
        }        
    }
}