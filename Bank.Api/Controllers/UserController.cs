using BankLibrary.Interfaces;
using BankLibrary.Models;
using BankLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bank.Api.DTOs;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _userService.GetAllUsersAsync();
            return Ok(userList);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var createdUser = await _userService.CreateUserAsync(createUserDTO.UserName, createUserDTO.Email, createUserDTO.Password);
            return Ok(new { message = "User created successfully", user = createdUser });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDTO)
        {
            var loggedinUser =  await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);

            if (loggedinUser == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new { message = "Login successful", user = loggedinUser });
        }
    }
}
