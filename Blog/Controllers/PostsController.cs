using Blog.DAL;
using Blog.Models;
using Blog.ViewModels;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext dbContext = new BlogContext();
        // GET: Posts
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostCreateViewModel postVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", postVM);
            }
            var post = new Post
            {
                Title = postVM.Title,
                Content = postVM.Content,
                AuthorName = postVM.AuthorName,
                Created = postVM.GetDataTime(),
            };


            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");


        }

    }
}

