﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.DTOs.Articles;
using YoutubBlog.Entity.Entities;

namespace YoutubeBlog.Service.AutoMapper.Articles
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDto,Article>().ReverseMap();
            CreateMap<ArticleUpdateDto,Article>().ReverseMap();
            CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
            CreateMap<ArticleAddDto, Article>().ReverseMap();
        }
    }
}
