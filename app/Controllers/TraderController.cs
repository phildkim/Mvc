using MediatR;
using MS.Features;
using MS.Model.View;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace app.Controllers
{
    public class TraderController : BaseController
    {
        public TraderController(IMediator mediator) : base(mediator) { }

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion Index

        #region Query
        [HttpGet]
        public async Task<JsonResult> Query()
        {
            var model = await Send(new Query());
            return Json(model);
        }
        #endregion Query

        #region Create
        [HttpPost]
        public async Task<JsonResult> Create(EditVM traderDto)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = await Send(new Create { EditVM = traderDto }).ConfigureAwait(ModelState.IsValid) });
            }
            return Json(new { success = false });
        }
        #endregion Create

        #region Update
        [HttpPost]
        public async Task<JsonResult> Update(int? id, EditVM traderDto)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = await Send(new Update { Id = id, EditVM = traderDto }).ConfigureAwait(ModelState.IsValid) });
            }
            return Json(new { success = false });
        }
        #endregion Update

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            return Json(new { success = await Send(new Delete { Id = id }) });
        }
        #endregion Delete
    }
}