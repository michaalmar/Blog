using Blog.DTO;
using Blog.Models;
using Blog.ViewModels;
using System.Collections.Generic;

namespace Blog.IServices
{
    public interface IPostsService
    {
        IEnumerable<PostDTO> GetAll();

        Post Get(int id);

        Post Delete(int id);

        bool Update(PostEditViewModel viewModel);

        Post Create(PostCreateViewModel viewModel);

    }
}
