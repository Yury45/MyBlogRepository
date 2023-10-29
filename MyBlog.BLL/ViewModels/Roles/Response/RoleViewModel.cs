using MyBlog.Data.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.ViewModels.Roles.Response
{
    /// <summary>
    /// Модель одной роли
    /// </summary>
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public bool IsSelected { get; set; }
    }
}
