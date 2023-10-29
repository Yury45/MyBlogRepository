using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.ViewModels.Comments.Request
{
    /// <summary>
    /// Модель создания комментария
    /// </summary>
    public class CreateCommentViewModel
    {
        /// <summary>
        /// Содержание комментария
        /// </summary>
        [Required(ErrorMessage = "Заполните поле: Содержание")]
        [DataType(DataType.Text)]
        [Display(Name = "Содержание", Prompt = "Содержание")]
        public string? Content { get; set; }

        /// <summary>
        /// Автор комментария
        /// </summary>
        [Required(ErrorMessage = "Заполните поле: Автор")]
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string? Author { get; set; }

        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public int ArticleId;
    }
}
