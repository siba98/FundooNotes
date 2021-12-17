namespace FundooRepository.Repository
{
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<string> Register(RegisterModel user)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == user.Email).SingleOrDefaultAsync();
                if (checkEmail == null)
                {
                    //Encrypt the password
                    user.Password = EncodePasswordToBase64(user.Password);
                    this.context.Users.Add(user);
                    await this.context.SaveChangesAsync();
                    return "Register Successful";
                }
                return "Email already exists";

            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> Login(LoginModel loginDetails)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == loginDetails.Email).SingleOrDefaultAsync();
                if (checkEmail != null)
                {
                    // Encrypt the password
                    loginDetails.Password = EncodePasswordToBase64(loginDetails.Password);
                    var checkPassword = await this.context.Users.Where(x => x.Email == loginDetails.Email && x.Password == loginDetails.Password).SingleOrDefaultAsync();
                    if (checkPassword != null)
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServerUrl"]);
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "First Name", checkEmail.FirstName);
                        database.StringSet(key: "Last Name", checkEmail.LastName);
                        database.StringSet(key: "Email", checkEmail.Email);
                        database.StringSet(key: "UserId", checkEmail.UserId.ToString());
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

        public async Task<string> ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == resetPassword.Email).SingleOrDefaultAsync();
                if (checkEmail != null)
                {
                    checkEmail.Password = EncodePasswordToBase64(resetPassword.NewPassword);
                    this.context.Users.Update(checkEmail);
                    await this.context.SaveChangesAsync();
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

        public async Task<string> ForgotPassword(string Email)
        {
            try
            {
                var checkEmail = await this.context.Users.Where(x => x.Email == Email).SingleOrDefaultAsync();
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