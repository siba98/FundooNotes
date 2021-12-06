using FundooManager.Interface;
using FundooModels;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Contollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                this.logger.LogInformation(user.FirstName + " is trying to Register");
                string message = await this.manager.Register(user);
                if (message.Equals("Register Successful"))
                {
                    this.logger.LogInformation(user.FirstName + " has successfully Registered");
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogInformation(user.FirstName + " registration was unsuccessful");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(user.FirstName + " had exception while registering : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginDetails)
        {
            try
            {
                string message = await this.manager.Login(loginDetails);
                if (message.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    string email = database.StringGet("Email");
                    int userId = Convert.ToInt32(database.StringGet("UserId"));

                    RegisterModel data = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserId = userId,
                        Email = email
                    };

                    string tokenString = this.manager.GenerateToken(loginDetails.Email);
                    return this.Ok(new { Status = true, Message = message, Data = data, Token = tokenString });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> Resetpassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                string message = await this.manager.ResetPassword(resetPassword);
                if (message.Equals("Password Successfully Reset"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                string message = await this.manager.ForgotPassword(Email);
                if (message.Equals("Reset Link Sent to Your Email Successfully"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
