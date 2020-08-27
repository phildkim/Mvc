namespace app.Controllers
{
    using MediatR;
    using MS.Model.View;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    public abstract class BaseController : Controller
    {
        private readonly IMediator mediator;
        protected BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        protected Task<T> Send<T>(IRequest<T> request)
        {
            return mediator.Send(request);
        }
        protected ActionResult NotValidJson(IEnumerable<UIAlert> valRes)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 400;
            return Json(valRes);
        }
        protected new JsonResult Json(object obj)
        {
            return new JsonResult()
            {
                ContentEncoding = System.Text.Encoding.UTF8,
                ContentType = "application/json",
                Data = obj,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
    }
}