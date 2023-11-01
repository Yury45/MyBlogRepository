using MyBlog.Data.Models.Articles;

namespace MyBlog.BLL.ViewModels.Articles.Response
{
    /// <summary>
    /// Модель списка статей
    /// </summary>
    public class ArticlesViewModel
    {
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
