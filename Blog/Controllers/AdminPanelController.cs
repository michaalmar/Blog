using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        [Authorize(Roles =Constant.Constant.User.ROLE_ADMIN)]
        public ActionResult Index()
        {
            return View();
        }
    }
}