// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------


namespace FundooNotes.Contollers
{
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// UserController class for Users API implementation
    /// </summary>
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
                this.logger.LogInformation(loginDetails.Email + " is trying to Login");
                string message = await this.manager.Login(loginDetails);
                if (message.Equals("Login Successful"))
                {
                    this.logger.LogInformation(loginDetails.Email + " is successfully Logged in");
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
                    this.logger.LogInformation(loginDetails.Email + " Login was unsuccessful");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(loginDetails.Email + " had exception while login : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> Resetpassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                this.logger.LogInformation(resetPassword.Email + " is trying to reset the password for given Email");
                string message = await this.manager.ResetPassword(resetPassword);
                if (message.Equals("Password Successfully Reset"))
                {
                    this.logger.LogInformation(resetPassword.Email + " Reset Password is Successful");
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogInformation(resetPassword.Email + " Failed to reset the Password ");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(resetPassword.Email + " had exception while reseting the Password : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                this.logger.LogInformation(Email + " the link for reseting the password has accessed");
                string message = await this.manager.ForgotPassword(Email);
                if (message.Equals("Reset Link Sent to Your Email Successfully"))
                {
                    this.logger.LogInformation(Email + " link has sent to given gmail to reset password successfully");
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogInformation(Email + " unable to sent the link! Email Id not exist");
                    return this.BadRequest(new { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(Email + " had exception while sending link to the mail : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}