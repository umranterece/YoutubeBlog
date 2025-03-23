using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.DTOs.Categories;
using YoutubBlog.Entity.Entities;

namespace YoutubeBlog.Service.AutoMapper.Categories
{
    public class CategoryProfile:Profile
    { 
        public CategoryProfile()
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<CategoryAddDto,Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }

    }
}
