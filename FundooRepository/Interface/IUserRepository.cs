using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        Task<string> Register(RegisterModel userData);
        Task<RegisterModel> Login(LoginModel loginDetails);
        //Task<string> Login(LoginModel loginDetails);
        Task<string> ResetPassword(ResetPasswordModel resetPassword);
        Task<bool> ForgotPassword(string email);
        //Task<string> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}