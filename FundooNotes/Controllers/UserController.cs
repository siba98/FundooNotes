// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes.Contollers
{
    using System;
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
        /// Constant field for SessionName and SessionEmail
        /// </summary>
        const string SessionName = "UserName";
        const string SessionEmail = "EmailId";

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
            try
            {
                this.logger.LogInformation(userData.FirstName + " is trying to Register");
                HttpContext.Session.SetString(SessionName, userData.FirstName + " " + userData.LastName);
                HttpContext.Session.SetString(SessionEmail, userData.Email);
                var result = await this.manager.Register(userData);
                if (result != null)
                {
                    var name = HttpContext.Session.GetString(SessionName);
                    var email = HttpContext.Session.GetString(SessionEmail);
                    this.logger.LogInformation(userData.FirstName + " has successfully Registered");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Registered Successfully", Data = result }); //SessionData = "Session details(FirstName, LastName, EmailId): "+name+" "+email,
                }
                else
                {
                    this.logger.LogInformation(userData.FirstName + " registration was unsuccessful");
                    return this.BadRequest(new { Status = false, Message = "User Registration is UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(userData.FirstName + " had exception while registering : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Login Api for user
        /// </summary>
        /// <param name="loginDetails">passing loginDetails parameter for LoginModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginData)
        {
            try
            {
                this.logger.LogInformation(loginData.Email + " is trying to Login");
                var result = await this.manager.Login(loginData);
                if(result != null)
                {
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
                    return this.Ok(new { Status = true, Message = "login successful", Data = data, Token = tokenString, Output = result });
                }
                else
                {
                    this.logger.LogInformation(loginData.Email + " Login was unsuccessful");
                    return this.BadRequest(new { Status = false, Message = "login unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(loginData.Email + " had exception while login : " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Resetpassword Api for user
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>response status from api</returns>
        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> Resetpassword(ResetPasswordModel resetPassword)
        {
            try
            {
                this.logger.LogInformation(resetPassword.Email + " is trying to reset the password for given Email");
                var result = await this.manager.ResetPassword(resetPassword);
                if (result != null)
                {
                    this.logger.LogInformation(resetPassword.Email + " Reset Password is Successful");
                    return this.Ok(new ResponseModel<ResetPasswordModel> { Status = true, Message = "Password Reset Successfully", Data = result });
                }
                else
                {
                    this.logger.LogInformation(resetPassword.Email + " Failed to reset the Password ");
                    return this.BadRequest(new { Status = false, Message = "Password Reset UnSuccessful" });
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
                    return this.Ok(new { Status = true, Message = "Link sent for reseting the password" }) ;
                }
                else
                {
                    this.logger.LogInformation(Email + " unable to sent the link! Email Id not exist");
                    return this.BadRequest(new { Status = false, Message = "unable to sent link" });
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