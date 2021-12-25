// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="A Siba Patro"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// interface IUserManager for user api's
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Register method for registration of new user
        /// </summary>
        /// <param name="userData">passing userData parameter for RegisterModel</param>
        /// <returns>Returns user that registered</returns>
        Task<RegisterModel> Register(RegisterModel userData);

        /// <summary>
        /// Login method for user login
        /// </summary>
        /// <param name="loginData">passing loginDetails parameter for LoginModel</param>
        /// <returns>return login details of user</returns>
        Task<LoginModel> Login(LoginModel loginData);

        /// <summary>
        /// method for reseting the password
        /// </summary>
        /// <param name="resetPassword">passing resetPassword parameter for ResetPasswordModel</param>
        /// <returns>return users reset password details</returns>
        Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword);

        /// <summary>
        /// method for getting reset link for Forgot Password
        /// </summary>
        /// <param name="Email">passing parameter as Email</param>
        /// <returns>returns boolean value</returns>
        Task<bool> ForgotPassword(string email);

        /// <summary>
        /// method for generating a token for authorization of api's
        /// </summary>
        /// <param name="Email">passing parameter as Email</param>
        /// <returns>returns jwt token</returns>
        string GenerateToken(string email);
    }
}
