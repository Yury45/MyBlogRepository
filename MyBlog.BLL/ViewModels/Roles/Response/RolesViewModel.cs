using MyBlog.Data.Models.Roles;

namespace MyBlog.BLL.ViewModels.Roles.Response
{
	/// <summary>
	/// Модель списка ролей
	/// </summary>
	public class RolesViewModel
    {
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
