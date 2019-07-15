﻿using System;

namespace Blog.ViewModels
{
    public class PostDetailsViewModel
    {
        public DateTime Created { get; set; } 

        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public byte[] Image { get; set; }
    }
}


