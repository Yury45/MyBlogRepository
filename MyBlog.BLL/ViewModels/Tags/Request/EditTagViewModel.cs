using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.ViewModels.Tags.Request
{
    /// <summary>
    /// Модель обновления тега
    /// </summary>
    public class EditTagViewModel
    {
        [Required(ErrorMessage = "Укажите Id тега!")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Укажите название тега!")]
        [Display(Name = "Название тега")]
        public string Name { get; set; }
    }
}
