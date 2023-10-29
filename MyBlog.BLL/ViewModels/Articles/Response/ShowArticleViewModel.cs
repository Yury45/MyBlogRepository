using MyBlog.BLL.ViewModels.Comments;
using MyBlog.Data.Models.Comments;
using MyBlog.Data.Models.Tags;
using MyBlog.Data.Models.Users;

namespace MyBlog.BLL.ViewModels.Articles.Response
{
    /// <summary>
    /// Модель статьи
    /// </summary>
    public class ShowArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
