using System.Threading.Tasks;
using FundooModels;

namespace FundooManager.Interface
{
    public interface IUserManager
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
