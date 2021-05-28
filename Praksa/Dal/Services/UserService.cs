using AutoMapper;
using Domain.Interfaces;
using DTO;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> user, IUserRepository repository, IMapper mapper, SignInManager<User> sigInManager)
        {
            _userManager = user;
            _userRepository = repository;
            _mapper = mapper;
            _signInManager = sigInManager;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _userRepository.Login(email, password);
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<UserDTO> UserDetails(string id)
        {
            var user = await _userRepository.UserDetails(id);
            UserDTO userdto = new UserDTO();

            userdto = _mapper.Map<UserDTO>(user);
            userdto.FirstName = user.UserName;
            //userdto.LastName = user.Lastname;
            return userdto;
        }

        public async Task Register(UserDTO user)
        {
            User user1 = _mapper.Map<User>(user);
            user1.Deleted = false;
            var result = await _userManager.CreateAsync(user1, user.Password);
            if (result.Succeeded)
            {
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user1);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user1.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation",
                //                          new { email = Input.Email });
                //}
                //else
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    return LocalRedirect(returnUrl);
                //}
            }

            //await _userManager.AddToRoleAsync(user1, "User");
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.DeleteUser(id);
        }

        public async Task AddPost(string id, string path, string title)
        {
            await _userRepository.AddPost(id, path, title);
        }

        public async Task Comment(string postId, string text)
        {
            await _userRepository.Comment(postId, text);
        }
    }
}