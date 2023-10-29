using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.ViewModels.Roles.Request
{
    /// <summary>
    /// Модель обновления роли
    /// </summary>
    public class EditRoleViewModel
    {
        [Required(ErrorMessage = "Укажите Id роли!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название роли!")]
        [Display(Name = "Название роли")]
        public string Name { get; set; }

        [Display(Name = "Описание роли")]
        public string? Description { get; set; }
    }
}
