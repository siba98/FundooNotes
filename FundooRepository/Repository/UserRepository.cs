// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;

    /// <summary>
    /// class UserRepository
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// object created for UserContext
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// object created for IConfiguration
        /// IConfiguration: Represents a set of key/value application configuration properties.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// this function Convert to Encode the Password 
        /// </summary>
        /// <param name="password">passing parameter as password</param>
        /// <returns>returns encoded password</returns>
        public static string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// Register method for user registration
        /// </summary>
        /// <param name="userData">passing userData parameter for RegisterModel</param>
        /// <returns>Returns user that registered</returns>
        public async Task<RegisterModel> Register(RegisterModel userData)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == userData.Email).SingleOrDefaultAsync();
                if (checkEmail == null)
                {
                    this.context.Users.Add(userData);
                    await this.context.SaveChangesAsync();
                    return userData;
                }

                throw 
            }
            catch (FundooNotesCustomException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Login method for user login
        /// </summary>
        /// <param name="loginDetails">passing loginDetails parameter for LoginModel</param>
        /// <returns>return login details of user</returns>
        public async Task<LoginModel> Login(LoginModel loginData)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == loginData.Email).SingleOrDefaultAsync();
                if (checkEmail != null)
                {
                    var checkPassword = await this.context.Users.Where(x => x.Email == loginData.Email && x.Password == loginData.Password).SingleOrDefaultAsync();
                    if (checkPassword != null)
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServerUrl"]);
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", checkEmail.FirstName);
                        database.StringSet(key: "Last Name", checkEmail.LastName);
                        database.StringSet(key: "Email", checkEmail.Email);
                        database.StringSet(key: "UserId", checkEmail.UserId.ToString());
                        return loginData;
                    }

                    return null;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for reset the password
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>return users reset password details</returns>
        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == resetPassword.Email).SingleOrDefaultAsync();
                if (checkEmail != null)
                {
                    checkEmail.Password = EncodePasswordToBase64(resetPassword.NewPassword);
                    this.context.Users.Update(checkEmail);
                    await this.context.SaveChangesAsync();
                    return resetPassword;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for getting reset link for Forgot Password
        /// </summary>
        /// <param name="email">passing parameter as email</param>
        /// <returns>returns boolean value</returns>
        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
                if (checkEmail != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(email);
                    SendMSMQ();
                    mail.Body = RecieveMSMQ();

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(mail);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method for sending message
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\Fundoo"))
            {
                msgqueue = new MessageQueue(@".\Private$\Fundoo");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\Fundoo");
            }

            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "Visit this link for reset your password => (http://localhost:4200/reset)";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        /// <summary>
        /// method for recieve message
        /// </summary>
        /// <returns>returns string message</returns>
        public string RecieveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var recievemsg = msgqueue.Receive();
            recievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return recievemsg.Body.ToString();
        }

        /// <summary>
        /// method for generating a token for authorization
        /// </summary>
        /// <param name="email">passing parameter as Email</param>
        /// <returns>returns jwt token</returns>
        public string GenerateToken(string Email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, Email)}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}