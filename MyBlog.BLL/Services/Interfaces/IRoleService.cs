using Microsoft.AspNetCore.Http;
using MyBlog.BLL.ViewModels.Roles.Request;
using MyBlog.BLL.ViewModels.Roles.Response;
using MyBlog.Data.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task<int> CreateRoleAsync(CreateRoleViewModel model);

        Task EditRoleAsync(EditRoleViewModel model);

        Task DeleteRoleAsync(int id);

        Task<List<Role>> GetRolesAsync();

        Task<Role?> GetRoleAsync(int id);
    }
}
