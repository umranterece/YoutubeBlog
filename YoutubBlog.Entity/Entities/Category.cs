using YoutubeBlog.Core.Entities;

namespace YoutubBlog.Entity.Entities
{
    public class Category : EntityBase
    {

        public Category()
        {
            
        }

        public Category(string name)
        {
            Name=name;
        }
        public string Name { get; set; }

        // Relational Properties
        public ICollection<Article> Articles { get; set; }

    }
}