using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Tags.Request
{
    /// <summary>
    /// Модель создания тега
    /// </summary>
    public class CreateTagViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите имя тега!")]
        [Display(Name = "Название")]
        public string Name { get ; set; }
    }
}
