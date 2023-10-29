using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Users.Request
{
    /// <summary>
    /// Модель авторизации пользователя
    /// </summary>
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите Password")]
        public string Password { get; set; }
    }
}
