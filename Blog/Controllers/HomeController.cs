using Blog.DAL;
using Blog.DTO;
using Blog.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext dbContext;

        public HomeController()
        {
            dbContext = new BlogContext();
        }

        public ActionResult Index()
        {
            var list = dbContext.Posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorName = p.AuthorName,


            }).ToList();

            var postsList = new PostsListViewModel
            {
                PostsList = list
            };

            return View(postsList);
        }



    }
}