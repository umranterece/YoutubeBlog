using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YoutubBlog.Entity.DTOs.Articles;
using YoutubBlog.Entity.Entities;
using YoutubBlog.Entity.Enums;
using YoutubeBlog.Data.UnitOfWorks;
using YoutubeBlog.Service.Extensions;
using YoutubeBlog.Service.Helpers.Images;
using YoutubeBlog.Service.Services.Abstractions;

namespace YoutubeBlog.Service.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageHelper imageHelper;
        private readonly ClaimsPrincipal _user;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor,IImageHelper imageHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.imageHelper = imageHelper;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
        {
            var userId = _user.GetLoggedInUserId();
            var userEmail=_user.GetLoggedInUserEmail();

            var imageUpload =await imageHelper.Upload(articleAddDto.Title,articleAddDto.Photo,ImageType.Post);
            Image image = new(imageUpload.FullName,articleAddDto.Photo.ContentType,userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);


            var article = new Article(articleAddDto.Title,articleAddDto.Content,userId, userEmail, articleAddDto.CategoryId,image.Id);

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
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id==articleId, x => x.Category, i=>i.Image);
            var map = mapper.Map<ArticleDto>(article);
            return map;
        }

        public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
        {
            var userEmail= _user.GetLoggedInUserEmail();
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => x.IsDeleted == false && x.Id == articleUpdateDto.Id, x => x.Category, i => i.Image);

            if (articleUpdateDto.Photo != null)
            {
                imageHelper.Delete(article.Image.FileName);
               
                var imageUpload = await imageHelper.Upload(articleUpdateDto.Title,articleUpdateDto.Photo,ImageType.Post);
                Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
                await unitOfWork.GetRepository<Image>().AddAsync(image);

                article.ImageId = image.Id;
            }

            article.Title=articleUpdateDto.Title;
            article.Content=articleUpdateDto.Content;
            article.CategoryId=articleUpdateDto.CategoryId;

            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = userEmail;

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

            return article.Title;
        }

        public async Task<string> SafeDeteleArticleAsync(Guid articleId)
        {
            var article=await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            article.IsDeleted=true;
            article.DeletedDate=DateTime.Now;
            article.DeletedBy= _user.GetLoggedInUserEmail();

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();  
            return article.Title;
        }
    }
}
