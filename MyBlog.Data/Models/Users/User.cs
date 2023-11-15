﻿using Microsoft.AspNetCore.Identity;
using MyBlog.Data.Models.Articles;
using MyBlog.Data.Models.Comments;

namespace MyBlog.Data.Models.Users
{
	/// <summary>
	/// Сущность пользователя
	/// </summary>
	public class User : IdentityUser<int>
    {
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
		public string Photo {  get; set; }

        public List<Article> Articles { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

        public User()
        {
            Photo = "https://thispersondoesnotexist.com/";
        }
    }
}
