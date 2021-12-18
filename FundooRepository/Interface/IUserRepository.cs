

namespace FundooRepository.Interface
{
    using FundooModels;
    using System.Threading.Tasks;
    public interface IUserRepository
    {
        //Task<bool> Register(RegisterModel userData);
        //Task<string> Register(RegisterModel userData);
        Task<RegisterModel> Register(RegisterModel userData);
        Task<string> Login(LoginModel loginDetails);
        Task<string> ResetPassword(ResetPasswordModel resetPassword);
        Task<string> ForgotPassword(string email);
        string GenerateToken(string email);
    }
}