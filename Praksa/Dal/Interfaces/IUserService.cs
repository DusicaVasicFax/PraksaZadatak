using DTO;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        public Task<User> Login(string email, string password);

        public Task Register(UserDTO user);

        public Task Logout();

        public Task<UserDTO> UserDetails(string id);

        public Task DeleteUser(string id);

        public Task Comment(string postId, string text);

        public Task AddPost(string id, string path, string title);
    }
}