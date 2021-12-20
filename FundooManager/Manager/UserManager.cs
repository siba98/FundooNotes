// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// User Manager Class for User Api's
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// object created for IUserRepository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the UserManager class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing userData parameter for RegisterModel</param>
        /// <returns>Returns string type</returns>
        public async Task<RegisterModel> Register(RegisterModel userData)
        {
            try
            {
                userData.Password = EncodePasswordToBase64(userData.Password);
                return await this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Login method for manager class
        /// </summary>
        /// <param name="loginDetails">passing loginDetails parameter for LoginModel</param>
        /// <returns>return string type</returns>
        public async Task<LoginModel> Login(LoginModel loginDetails)
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

        /// <summary>
        /// this function Convert to Encode the Password 
        /// </summary>
        /// <param name="Password">passing parameter as Password</param>
        /// <returns>returns string type</returns>
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
        /// method for reseting the password
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>return string type</returns>
        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword)
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

        /// <summary>
        /// method for getting reset link for Forgot Password
        /// </summary>
        /// <param name="Email">passing parameter as Email</param>
        /// <returns>returns string type</returns>
        public async Task<bool> ForgotPassword(string Email)
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

        /// <summary>
        /// method for generating a token for authorization of api's
        /// </summary>
        /// <param name="Email">passing parameter as Email</param>
        /// <returns>returns string type</returns>
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