using MyBlog.Data.Models.Articles;

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
