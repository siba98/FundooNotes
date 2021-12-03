using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        string Register(RegisterModel user);
        string Login(LoginModel loginDetails);
        string ResetPassword(ResetPasswordModel resetPassword);
        string ForgotPassword(string email);
        string GenerateToken(string email);
    }
}
