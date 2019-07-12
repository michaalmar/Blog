using Blog.DAL;
using Blog.Models;
using Blog.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext dbContext;

        public PostsController()
        {
            dbContext = new BlogContext();
        }

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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = dbContext.Posts.Find(id);
            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = dbContext.Posts.Find(id);

            var postVM = new PostDetailsViewModel
            {
                AuthorName = post.AuthorName,
                Content = post.Content,
                Title = post.Title,
            };

            return View(postVM);

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = dbContext.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var postVM = new PostEditViewModel
            {
                Content = post.Content,
                Title = post.Title,
            };


            return View(postVM);

        }

        [HttpPost]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postToUpdate = dbContext.Posts.Find(id);

            if (TryUpdateModel(postToUpdate, new string[] { "Content", "Title" }))
            {
                dbContext.SaveChanges();
               return RedirectToAction("Index", "Home");
            }

            var newPostVm = new PostEditViewModel
            {
                Content = postToUpdate.Content,
                Title = postToUpdate.Title,
            };

            return View(newPostVm);


        }

    }
}
