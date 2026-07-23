using PrimeStore.data.Entities.Identity;

namespace PrimeStore.service.Abstracts
{
    public interface IUserService
    {
        Task<string> AddUserAsync(User user, string password);

        Task<string> UpdateUserStatus(int UserId, int StatusId);
    }
}
