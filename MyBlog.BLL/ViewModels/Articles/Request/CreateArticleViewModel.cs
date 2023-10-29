using MyBlog.BLL.ViewModels.Tags.Response;
using MyBlog.Data.Models.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.ViewModels.Articles.Request
{
    /// <summary>
    /// Модель создания статьи
    /// </summary>
    public class CreateArticleViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Теги", Prompt = "Теги")]
        public List<TagViewModel>? Tags { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string? Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }
    }
}
