using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel userData);
        Task<LoginModel> Login(LoginModel loginDetails);
        Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword);
        Task<bool> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}
