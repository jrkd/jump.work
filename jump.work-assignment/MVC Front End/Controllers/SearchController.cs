
using System.Web.Mvc;

namespace jump.work.mvc.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Test()
        {
            return View("Index");
        }
        //[HttpGet]
        //public ActionResult Reports()
        //{
        //    if( Request.QueryString["query"] != null)
        //    {
        //        return Reports(new ReportSearchResultsVM()
        //        {
        //            Query = Request.QueryString["query"],
        //            IsGlobalSearch = true
        //        });
        //    }
        //    else
        //    {
        //        return View("Index", GetResultsVM(SearchType.Reports));
        //    }
        //}
        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}