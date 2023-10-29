using AutoMapper;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Comments.Request;
using MyBlog.Data.Models.Comments;
using MyBlog.Data.Repositories;
using MyBlog.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BLL.Services
{
    public class CommentService : ICommentService
    {
        public IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CommentRepository _commentRepository;

        public CommentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _commentRepository = (CommentRepository)unitOfWork.GetRepository<Comment>();
        }

        public async Task<int> CreateCommentAsync(CreateCommentViewModel model, int userId)
        {
            var comment = new Comment
            {
                Text = model.Content,
                CreatedDate = DateTime.Now,
                ArticleId = model.ArticleId,
                UserId = userId

            };
            await _commentRepository.CreateAsync(comment);
            return comment.Id;
        }

        public async Task<EditCommentViewModel> EditCommentAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            var result = new EditCommentViewModel
            {
                Content = comment.Text,
            };

            return result;
        }

        public async Task EditCommentAsync(EditCommentViewModel model, int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            comment.Text = model.Content;

            await _commentRepository.UpdateAsync(comment);
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = _commentRepository.GetByIdAsync(id);

            await _commentRepository.DeleteAsync(await comment);
        }
    }
}
