using System.Threading.Tasks;
using JournalAPI.Models;

namespace JournalAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginRequest request);
        Task<AuthResult> RegisterAsync(RegisterRequest request);
    }
}
