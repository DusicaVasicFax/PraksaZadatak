using Domain.Interfaces;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praksa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            try
            {
                User user1 = await _userService.Register(user);
                return Ok(user1);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}