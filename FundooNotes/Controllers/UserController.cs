using FundooManager.Interface;
using FundooModels;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Contollers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                string message = this.manager.Register(userData);
                if (message.Equals("You Registered Successfully"))
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
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
                string message = this.manager.Login(loginData);
                if (message.Equals("Login Successful"))
                {
                    string tokenString = this.manager.GenerateToken(loginData.Email);
                    return this.Ok(new { Status = true, Message = message, Token = tokenString });
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

        [HttpPut]
        [Route("api/resetPassword")]
        public IActionResult Resetpassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                string message = this.manager.ResetPassword(resetPassword);
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
        [Route("api/forgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                string message = this.manager.ForgotPassword(Email);
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
