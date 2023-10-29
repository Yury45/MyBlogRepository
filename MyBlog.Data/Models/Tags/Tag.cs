using MyBlog.Data.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Models.Tags
{
    /// <summary>
    /// Сущность тега
    /// </summary>
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
