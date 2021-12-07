﻿using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel user);
        Task<string> Login(LoginModel loginDetails);
        Task<string> ResetPassword(ResetPasswordModel resetPassword);
        Task<string> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}
