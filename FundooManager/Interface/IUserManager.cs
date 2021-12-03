using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel user);
        string Login(LoginModel loginDetails);
        string ResetPassword(ResetPasswordModel resetPassword);
        string ForgotPassword(string email);
        string GenerateToken(string email);
    }
}
