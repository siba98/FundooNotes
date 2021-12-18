using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel userData);
        Task<string> Login(LoginModel loginDetails);
        Task<string> ResetPassword(ResetPasswordModel resetPassword);
        Task<string> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}
