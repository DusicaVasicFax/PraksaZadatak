using AutoMapper;
using Domain.Interfaces;
using DTO;
using Infrastructure.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _userService.Register(userDto);
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoggingUser user)
        {
            var res = await _userService.Login(user.Email, user.Password);
            if (res == null)
                return Unauthorized();

            var Claims = new List<Claim>
            {
                new Claim("type", "User"),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

            var Token = new JwtSecurityToken(
                "https://localhost:44395/instagram",
                "https://localhost:44395/instagram",
                Claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(Token);

            return Ok(new { Token = token });
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _userService.Logout();
            return Ok();
        }

        [Route("details/{id}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UserDetails([FromRoute] string id)
        {
            var result = await _userService.UserDetails(id);
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeleteUser([FromRoute] string id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }

        [Route("comment/{postId}")]
        [HttpPost]
        public async Task<ActionResult> PostComment([FromRoute] string postId, [FromForm] string text)
        {
            await _userService.Comment(postId, text);
            return Ok();
        }

        [Route("post/{id}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> PostPicture([FromRoute] string id, [FromForm] string title)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    await _userService.AddPost(id, dbPath, title);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}