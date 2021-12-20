using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel userData);
        Task<LoginModel> Login(LoginModel loginDetails);
        Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword);
        Task<bool> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}