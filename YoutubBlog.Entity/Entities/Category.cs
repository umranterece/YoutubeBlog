using YoutubeBlog.Core.Entities;

namespace YoutubBlog.Entity.Entities
{
    public class Category : EntityBase
    {

        public Category()
        {
            
        }

        public Category(string name,string createdBy)
        {
            Name=name;
            CreatedBy=createdBy;
        }
        public string Name { get; set; }

        // Relational Properties
        public ICollection<Article> Articles { get; set; }

    }
}