using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Models.Comments
{
    /// <summary>
    /// Сущность комментария к статье
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }

        public Article Article { get; set; }
        public int ArticleId {  get; set; }
        public User User {  get; set; }
        public int UserId {  get; set; }
    }
}
