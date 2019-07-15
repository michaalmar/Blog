using Blog.DAL;
using Blog.DTO;
using Blog.IServices;
using Blog.Models;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.Services
{
    public class PostsService : IPostsService
    {
        private readonly BlogContext dbContext;

        public PostsService(BlogContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Post Create(PostCreateViewModel viewModel, HttpPostedFileBase file )
        {
            viewModel.Image = ConvertToBytes(file);

            var post = new Post
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                AuthorName = viewModel.AuthorName,
                Created = DateTime.UtcNow,
                Image = viewModel.Image,
            };

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            return post;
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;

        }



        public Post Delete(int id)
        {
            Post post = dbContext.Posts.Find(id);

            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();

            return post;

        }

        public bool Update(PostEditViewModel postVM)
        {
            var post = dbContext.Posts.Where(p => p.Id == postVM.Id).Single();

            post.Title = postVM.Title;
            post.Content = postVM.Content;

            dbContext.SaveChanges();

            return true;                               
        }


        public Post Get(int id) => dbContext.Posts.Find(id);


        public IEnumerable<PostDTO> GetAll() => dbContext.Posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorName = p.AuthorName,
                Image = p.Image,


            }).ToList();

        //public byte[] GetImageFromDataBase(int Id)
        //{
        //    var q = from temp in dbContext.Posts where temp.Id == Id select temp.Image;
        //    byte[] cover = q.First();
        //    return cover;
        //}
    }
}