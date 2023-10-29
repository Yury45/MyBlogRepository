using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.Data.Models.Roles;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Users.Request
{
    /// <summary>
    /// Модель создания пользователя
    /// </summary>
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Поле логин обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Логин", Prompt = "Введите логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле Почта обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
