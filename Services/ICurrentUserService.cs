using System.Security.Claims;

namespace JournalAPI.Services
{
    public interface ICurrentUserService
    {
        Guid GetUserId();
    }
}