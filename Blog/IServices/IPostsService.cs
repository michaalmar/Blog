using Blog.DTO;
using Blog.Models;
using Blog.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace Blog.IServices
{
    public interface IPostsService
    {
        IEnumerable<PostDTO> GetAll();

        Post Get(int id);

        void Delete(int id);

        bool Update(PostEditViewModel viewModel);

        Post Create(PostCreateViewModel viewModel, HttpPostedFileBase file );

      //  byte[] GetImageFromDataBase(int Id);

    }
}
