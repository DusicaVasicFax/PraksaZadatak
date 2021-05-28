using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext databaseContext;
        protected readonly DbSet<User> _entities;
        protected readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private Random _random;

        public UserRepository(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> sigInManager)
        {
            databaseContext = context;
            _entities = context.Set<User>();
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = sigInManager;
            _random = new Random();
        }

        public async Task AddPost(string id, string path, string title)
        {
            var userTask = _userManager.FindByIdAsync(id);
            var user = userTask.Result;
            if (user == null)
            {
                return;
            }
            else
            {
                Post newPost = new Post();
                newPost.Creator = user.UserName;
                newPost.Image = path + '-' + $"{DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}";
                newPost.Id = _random.Next(1, 10000).ToString();
                newPost.Title = title;

                var userDatabase = await databaseContext.Users.Include(x => x.posts).Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                //if (userDatabase.posts == null)
                //{
                //    userDatabase.posts = new List<Post>();
                //}
                newPost.user = userDatabase;
                //userDatabase.posts.Add(newPost);
                await databaseContext.Posts.AddAsync(newPost);
                try
                {
                    await databaseContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var a = ex;
                }
            }
        }

        public async Task Comment(string postId, string text)
        {
            var post = await databaseContext.Posts.Include(x => x.user).Where(x => x.Id.Equals(postId)).FirstOrDefaultAsync();
            var comment = new Comments { Id = _random.Next(1, 10000).ToString(), Author = post.user.UserName, Text = text, Timestamp = DateTime.Now, Post = post };
            if (post.Comments == null)
            {
                post.Comments = new List<Comments>();
            }
            post.Comments.Add(comment);
            databaseContext.Posts.Update(post);
            databaseContext.comments.Add(comment);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<User> DeleteUser(string id)
        {
            //var userTask = databaseContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            var userTask = _userManager.FindByIdAsync(id);
            var user = userTask.Result;
            if (user != null)
            {
                string date = $"{DateTime.Now.Date} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}";
                user.UserName = user.UserName + '-' + date;
                user.Deleted = true;
                databaseContext.Users.Update(user);
                databaseContext.SaveChanges();
                return user;
            }

            return null;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email.ToUpper());
            if (user == null)
            {
                return null;
            }
            var res = await _signInManager.PasswordSignInAsync(user.UserName,
                          password, false, false);
            if (!res.Succeeded)
            {
                return null;
            }

            return user;
        }

        public Task<User> UserDetails(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }
    }
}