using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> Register(RegisterModel user)
        {
            try
            {
                user.Password = EncodePasswordToBase64(user.Password);
                return await this.repository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Login(LoginModel loginDetails)
        {
            try
            {
                loginDetails.Password = EncodePasswordToBase64(loginDetails.Password);
                return await this.repository.Login(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //this function Convert to Encode the Password 
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

        public async Task<string> ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                resetPassword.NewPassword = EncodePasswordToBase64(resetPassword.NewPassword);
                return await this.repository.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ForgotPassword(string Email)
        {
            try
            {
                return await this.repository.ForgotPassword(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateToken(string Email)
        {
            try
            {
                return this.repository.GenerateToken(Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}