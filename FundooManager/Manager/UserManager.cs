using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public string Register(RegisterModel userData)
        {
            try
            {
                userData.Password = EncodePasswordToBase64(userData.Password);
                return this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(LoginModel loginData)
        {
            try
            {
                loginData.Password = EncodePasswordToBase64(loginData.Password);
                return this.repository.Login(loginData);
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

        public string ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                resetPassword.NewPassword = EncodePasswordToBase64(resetPassword.NewPassword);
                return this.repository.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(string Email)
        {
            try
            {
                return this.repository.ForgotPassword(Email);
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
