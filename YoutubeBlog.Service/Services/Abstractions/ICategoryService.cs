using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.DTOs.Articles;
using YoutubBlog.Entity.DTOs.Categories;

namespace YoutubeBlog.Service.Services.Abstractions
{
    public interface ICategoryService
    {
       public Task<List<CategoryDto>> GetAllCategoriesNonDeleted();

    }
}
