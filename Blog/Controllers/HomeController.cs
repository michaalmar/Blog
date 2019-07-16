using Blog.IServices;
using Blog.ViewModels;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsService postsService;

        public HomeController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public ActionResult Index()
        {

            var postsList = new PostsListViewModel
            {
                PostsList = postsService.GetAll()
            };

            return View(postsList);

        }
        //test//

    }


}