using Experimental.System.Messaging;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        //IConfiguration: Represents a set of key/value application configuration properties.
        private readonly IConfiguration configuration;

        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public string Register(RegisterModel user)
        {
            try
            {
                var checkEmail = this.context.Users.Where(x => x.Email == user.Email).SingleOrDefault();
                if (checkEmail == null)
                {
                    this.context.Users.Add(user);
                    this.context.SaveChanges();
                    return "You Registered Successfully";
                }
                return "Email is already exists in the Database";

            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string Login(LoginModel loginDetails)
        {
            try
            {
                var checkEmail = this.context.Users.Where(x => x.Email == loginDetails.Email).SingleOrDefault();
                if (checkEmail != null)
                {
                    var checkPassword = this.context.Users.Where(x => x.Password == loginDetails.Password).SingleOrDefault();
                    if (checkPassword != null)
                    {
                        return "Login Successful";
                    }
                    return "Password Not Exist";
                }
                return "Email not exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var checkEmail = this.context.Users.Where(x => x.Email == resetPassword.Email).SingleOrDefault();
                if (checkEmail != null)
                {
                    checkEmail.Password = EncodePasswordToBase64(resetPassword.NewPassword);
                    this.context.Users.Update(checkEmail);
                    this.context.SaveChanges();
                    return "Password Successfully Reset";
                }
                return "Email not Exist";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public string ForgotPassword(string Email)
        {
            try
            {
                var checkEmail = this.context.Users.Where(x => x.Email == Email).SingleOrDefault();
                if (checkEmail != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(Email);
                    SendMSMQ();
                    mail.Body = RecieveMSMQ();

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return "Reset Link Sent to Your Email Successfully";
                }
                return "Email not Exist";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
            string body = "This is Password reset link.";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        public string RecieveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var recievemsg = msgqueue.Receive();
            recievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return recievemsg.Body.ToString();
        }

        public string GenerateToken(string Email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, Email)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}

