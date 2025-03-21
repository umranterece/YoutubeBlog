﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.DTOs.Articles;
using YoutubBlog.Entity.Entities;
using YoutubeBlog.Data.UnitOfWorks;
using YoutubeBlog.Service.Services.Abstractions;

namespace YoutubeBlog.Service.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            var userId = Guid.Parse("CB94223B-CCB8-4F2F-93D7-0DF96A7F065C");
            var imageId=Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214");
            var article = new Article(articleAddDto.Title,articleAddDto.Content,userId,articleAddDto.CategoryId,imageId);

            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
        {
           var articles= await unitOfWork.GetRepository<Article>().GetAllAsync(x=>x.IsDeleted==false,x=>x.Category);
           var map=mapper.Map<List<ArticleDto>>(articles);
           return map;
        }

        public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id==articleId, x => x.Category);
            var map = mapper.Map<ArticleDto>(article);
            return map;
        }

        public async Task UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id == articleUpdateDto.Id, x => x.Category);
           
            article.Title=articleUpdateDto.Title;
            article.Content=articleUpdateDto.Content;
            article.CategoryId=articleUpdateDto.CategoryId;

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task SafeDeteleArticleAsync(Guid articleId)
        {
            var article=await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            article.IsDeleted=true;
            article.DeletedDate=DateTime.Now;
            // article.DeletedBy="Admin";

          await unitOfWork.GetRepository<Article>().UpdateAsync(article);
          await unitOfWork.SaveAsync();  
        }
    }
}
