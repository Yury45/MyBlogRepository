using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Tags.Response;
using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Comments;
using MyBlog.Data.Models.Tags;
using MyBlog.Data.Models.Users;
using MyBlog.Data.Repositories;
using MyBlog.Data.Repositories.Interfaces;


namespace MyBlog.BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userService;
        private readonly ArticleRepository _articleRepository;
        private readonly TagRepository _tagRepository;
        private readonly CommentRepository _commentRepository;


        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userService)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _articleRepository = (ArticleRepository)_unitOfWork.GetRepository<Article>();
            _commentRepository = (CommentRepository)_unitOfWork.GetRepository<Comment>(); 
            _tagRepository = (TagRepository)_unitOfWork.GetRepository<Tag>();
        }

        public async Task<CreateArticleViewModel> CreateArticleAsync()
        {
            var article = new Article();

            var tags = (await _tagRepository.GetAllAsync())
                .Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();

            var model = new CreateArticleViewModel
            {
                Title = article.Title = string.Empty,
                Content = article.Content = string.Empty,
                Tags = tags,
            };
            
            return model;
        }

        public async Task<int> CreateArticleAsync(CreateArticleViewModel model)
        {
            var tags = new List<Tag>();

            if (model.Tags != null)
            {
                var articleTags = model.Tags.Where(t => t.IsSelected == true).ToList();
                var tagsId = articleTags.Select(t => t.Id).ToList();
                tags = (await _tagRepository.GetAllAsync()).Where(t => tagsId.Contains(t.Id)).ToList();
            }

            var article = new Article
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                Tags = tags,
                UserId = model.AuthorId
            };

            var user = await _userService.FindByIdAsync(model.AuthorId.ToString());
            user.Articles.Add(article);

            await _articleRepository.CreateAsync(article);
            await _userService.UpdateAsync(user);

            return article.Id;
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            _articleRepository.DeleteAsync(article);
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllAsync();
        }

        public async Task<Article> ShowArticleAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            var user = await _userService.FindByIdAsync(article.UserId.ToString());
            var comments = _commentRepository.GetByArticleIdAsync(article.Id);
            article.Id = id;
            foreach (var comment in await comments)
            {
                if (article.Comments.FirstOrDefault(c => c.Id == comment.Id) == null)
                    article.Comments.Add(comment);
            }
            if (user.Id != null)
            {
                //article.UserId = user.Id;
            }
            
            return article;
        }

        public async Task<EditArticleViewModel> UpdateArticleAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            var tags = (await _tagRepository.GetAllAsync()).Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();
            foreach (var tag in tags)
            {
                if (tags != null)
                {
                    foreach (var articleTag in article.Tags)
                    {
                        if (articleTag.Id != tag.Id) continue;
                        tag.IsSelected = true;
                        break;
                    }
                }
            }
            var model = new EditArticleViewModel()
            {
                Id = id,
                Title = article.Title,
                Content = article.Content,
                Tags = tags
            };
            return model;
        }

        public async Task UpdateArticleAsync(EditArticleViewModel model, int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            article.Title = model.Title;
            article.Content = model.Content;
            foreach (var tag in model.Tags)
            {
                var tagChanged = await _tagRepository.GetByIdAsync(tag.Id);
                if (tag.IsSelected)
                {
                    article.Tags.Add(tagChanged);
                }
                else
                {
                    article.Tags.Remove(tagChanged);
                }
            }
            await _articleRepository.UpdateAsync(article);
        }
    }
}
