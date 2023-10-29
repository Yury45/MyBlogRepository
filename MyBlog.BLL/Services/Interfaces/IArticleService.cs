using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Articles.Response;
using MyBlog.Data.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.Services.Interfaces
{
    public interface IArticleService
    {
        /// <summary>
        /// Метод создания статьи
        /// </summary>
        Task<CreateArticleViewModel> CreateArticleAsync();

        /// <summary>
        /// Метод создания статьи
        /// </summary>
        Task<int> CreateArticleAsync(CreateArticleViewModel model);

        /// <summary>
        /// Метод обновления статьи
        /// </summary>
        Task<EditArticleViewModel> UpdateArticleAsync(int id);

        /// <summary>
        /// Метод обновления статьи
        /// </summary>
        Task UpdateArticleAsync(EditArticleViewModel model, int id);

        /// <summary>
        /// Метод удаления статьи
        /// </summary>
        Task DeleteArticleAsync(int id);

        /// <summary>
        /// Метод получения всех статьи
        /// </summary>
        Task<List<Article>> GetAllArticlesAsync();

        /// <summary>
        /// Метод показа статьи
        /// </summary>
        Task<Article> ShowArticleAsync(int id);
    }
}
