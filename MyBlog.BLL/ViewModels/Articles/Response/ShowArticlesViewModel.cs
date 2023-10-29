using MyBlog.Data.Models.Articles;

namespace MyBlog.BLL.ViewModels.Articles.Response
{
    /// <summary>
    /// Модель списка статей
    /// </summary>
    public class ShowArticlesViewModel
    {
        public List<Article> Article { get; set; } = new List<Article>();
    }
}
