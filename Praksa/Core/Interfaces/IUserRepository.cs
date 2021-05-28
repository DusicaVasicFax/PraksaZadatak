using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);

        Task<User> UserDetails(string id);

        Task<User> DeleteUser(string id);

        Task Comment(string postId, string text);

        Task AddPost(string id, string path, string title);
    }
}