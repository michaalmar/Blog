using Blog.DTO;
using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class PostsListViewModel
    {
        public IEnumerable<PostDTO> PostsList { get; set; }

    }
}