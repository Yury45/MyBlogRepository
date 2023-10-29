﻿using MyBlog.BLL.ViewModels.Comments.Request;
using MyBlog.Data.Models.Comments;


namespace MyBlog.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<int> CreateCommentAsync(CreateCommentViewModel model, int userId);

        Task DeleteCommentAsync(int id);

        Task<List<Comment>> GetCommentsAsync();

        Task<EditCommentViewModel> EditCommentAsync(int id);

        Task EditCommentAsync(EditCommentViewModel model, int id);
    }
}
