// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Contollers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// UserController class for Users API implementation
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Object created for IUserManager
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// Object created for IConfiguration 
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Object created for ILogger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
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
            this.logger.LogInformation(userData.FirstName + " is trying to Register");
            var result = await this.manager.Register(userData);
            this.logger.LogInformation(userData.FirstName + " has successfully Registered");
            return Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Registered Successfully", Data = result });
        }

        /// <summary>
        /// Login Api for user
        /// </summary>
        /// <param name="loginData">passing loginData parameter for LoginModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginData)
        {
            this.logger.LogInformation(loginData.Email + " is trying to Login");
            var result = await this.manager.Login(loginData);
            this.logger.LogInformation(loginData.Email + " is successfully Logged in");
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

            string tokenString = this.manager.GenerateToken(loginData.Email);
            return this.Ok(new { Status = true, Message = "login successful", Data = data, Token = tokenString });
        }

        /// <summary>
        /// Resetpassword Api for user
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>response status from api</returns>
        [HttpPut]
        [Route("resetPassword")]
        public async Task<IActionResult> Resetpassword(ResetPasswordModel resetPassword)
        {
            this.logger.LogInformation(resetPassword.Email + " is trying to reset the password for given Email");
            var result = await this.manager.ResetPassword(resetPassword);
            this.logger.LogInformation(resetPassword.Email + " Reset Password is Successful");
            return this.Ok(new ResponseModel<ResetPasswordModel> { Status = true, Message = "Password Reset Successfully", Data = result });
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
            this.logger.LogInformation(Email + " the link for reseting the password has accessed");
            bool result = await this.manager.ForgotPassword(Email);
            this.logger.LogInformation(Email + " link has sent to given gmail to reset password successfully");
            return StatusCode(StatusCodes.Status200OK, new { message = "Link sent for reseting the password" });
        }
    }
}