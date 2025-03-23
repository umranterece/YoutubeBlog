using YoutubBlog.Entity.Enums;
using YoutubeBlog.Core.Entities;

namespace YoutubBlog.Entity.Entities
{
    public class Image : EntityBase
    {
        public Image()
        {
            
        }

        public Image(string fileName, string fileType,string createdBy)
        {
            FileName=fileName;
            FileType=fileType;
            CreatedBy=createdBy;
        }
        public string FileName { get; set; }
        public string FileType { get; set; }

        // Relational Properties
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}