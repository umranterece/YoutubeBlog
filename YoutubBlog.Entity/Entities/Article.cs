﻿using YoutubeBlog.Core.Entities;

namespace YoutubBlog.Entity.Entities
{
    public class Article:EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Guid? ImageId { get; set; }

        // Relational Properties
        public Category Category { get; set; }
        public Image Image { get; set; }
     
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

    }
}
