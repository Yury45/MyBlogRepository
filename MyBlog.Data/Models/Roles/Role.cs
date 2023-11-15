using Microsoft.AspNetCore.Identity;

namespace MyBlog.Data.Models.Roles
{
	/// <summary>
	/// Сущность роли пользователя
	/// </summary>
	public class Role : IdentityRole<int>
    {
        public string? Description {  get; set; }

        //public List<User> Users { get; set; } = new();

    }
}
