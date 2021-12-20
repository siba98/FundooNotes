﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Contollers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// UserController class for Users API implementation
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Object created for IUserManager
        /// </summary>
        private readonly IUserManager manager;

        const string SessionName = "UserName";
        const string SessionMail = "EmailId";

        /// <summary>
        /// Object created for IConfiguration 
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Object created for ILogger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the UserController class
        /// </summary>
        /// <param name="manager">parameter manager for IUserManager</param>
        /// <param name="logger">parameter logger for ILogger</param>
        /// <param name="configuration">parameter configuration for IConfiguration</param>
        public UserController(IUserManager manager, ILogger<UserController> logger, IConfiguration configuration)
        {
            this.manager = manager;
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Register Api for user
        /// </summary>
        /// <param name="userData">passing userData parameter for RegisterModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData)
        {
            try
            {
                this.logger.LogInformation(userData.FirstName + " is trying to Register");
                HttpContext.Session.SetString(SessionName, userData.FirstName + " " + userData.LastName);
                string message = await this.manager.Register(userData);
                if (message.Equals("Register Successful"))
                {
                    var firstName = HttpContext.Session.GetString(SessionName);
                    var lastName = HttpContext.Session.GetString(SessionName);
                    var email = HttpContext.Session.GetString(SessionMail);
                    this.logger.LogInformation(userData.FirstName + " has successfully Registered");
                    return this.Ok(new ResponseModel<string> { Status = true, Message = message, Data = "Session details :"+firstName+" "+lastName+" "+email });
                }
                else
                {
                    this.logger.LogInformation(userData.FirstName + " registration was unsuccessful");
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(userData.FirstName + " had exception while registering : " + ex.Message);
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Login Api for user
        /// </summary>
        /// <param name="loginDetails">passing loginDetails parameter for LoginModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginDetails)
        {
            try
            {
                this.logger.LogInformation(loginDetails.Email + " is trying to Login");
                var result = await this.manager.Login(loginDetails);
                //string message = await this.manager.Login(loginDetails);
                if(result != null)
                //if (message.Equals("Login Successful"))
                {
                    this.logger.LogInformation(loginDetails.Email + " is successfully Logged in");
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServerUrl"]);
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
                    return this.Ok(new { Status = true, Message = "login successful", Data = data, Token = tokenString, Data2 = result });//
                }
                else
                {
                    this.logger.LogInformation(loginDetails.Email + " Login was unsuccessful");
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "login unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(loginDetails.Email + " had exception while login : " + ex.Message);
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resetpassword Api for user
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>response status from api</returns>
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

        /// <summary>
        /// Forgot password api for user
        /// </summary>
        /// <param name="Email">passing parameter Email</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                this.logger.LogInformation(Email + " the link for reseting the password has accessed");
                bool result = await this.manager.ForgotPassword(Email);
                if (result == true)
                {
                    this.logger.LogInformation(Email + " link has sent to given gmail to reset password successfully");
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Link sent for reseting the password" }) ;
                }
                else
                {
                    this.logger.LogInformation(Email + " unable to sent the link! Email Id not exist");
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "unable to sent link" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(Email + " had exception while sending link to the mail : " + ex.Message);
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}