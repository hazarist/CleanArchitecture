using CelanArchitecture.Core.Requests;
using CleanArchitecture.Core.Entities;

namespace CelanArchitecture.Core.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User CreateUser(RegistrationRequest request);
        string GenerateToken(User user);
        bool IsUserExist(string username);
        byte[] GetSalt();
        string HashPassword(string password, byte[] salt);
    }
}
