using MyBlog.Data.Models.Comments;
using MyBlog.Data.Models.Tags;
using MyBlog.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Models.Articles
{
    /// <summary>
    /// Сущность статьи
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }

        public Article()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
