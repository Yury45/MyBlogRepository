using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Roles.Request
{
    /// <summary>
    /// Модель создания роли
    /// </summary>
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Укажите название роли!")]
        [Display(Name = "Название роли")]
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        /// <example>description</example>
        [Display(Name = "Описание роли")]
        public string? Description { get; set; }
    }
}

