using MediatR;
using MS.Model.View;
using System.Web.Mvc;
namespace app.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMediator mediator) : base(mediator) { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Get()
        {
            HomeVM model = Send(new MS.Features.Home.Query()).Result;
            return Json(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}