using MyBlog.BLL.ViewModels.Users.Request;

namespace MyBlog.App.ViewModels
{
    /// <summary>
    /// Модель представления главной страницы
    /// </summary>
    public class MainPageViewModel
    {
        public RegisterUserViewModel RegisterUserViewModel { get; set; }

        public LoginUserViewModel LoginUserViewModel { get; set; }

        public MainPageViewModel()
        {
            RegisterUserViewModel = new RegisterUserViewModel();
            LoginUserViewModel = new LoginUserViewModel();
        }
    }
}
