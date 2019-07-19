using Blog.IServices;
using Blog.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }


        [Authorize(Roles ="Edytor")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Edytor")]
        public ActionResult Create(PostCreateViewModel post)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];


            if (ModelState.IsValid)
            {
                postsService.Create(post,file);

            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Delete(int id)
        {

            postsService.Delete(id);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
       // [Route("{id}/{Title}")]
        public ActionResult Details(int id)
        {
            var post = postsService.Get(id);

            var postVM = new PostDetailsViewModel
            {
                Id = post.Id,
                AuthorName = post.AuthorName,
                Content = post.Content,
                Title = post.Title,
                Created = post.Created,
                Image = post.Image,
            };

            return View(postVM);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = postsService.Get(id);

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
        public ActionResult Edit(PostEditViewModel postVM)
        {

            postsService.Update(postVM);

            return RedirectToAction("Index","Home");
        }


       

    }
}
