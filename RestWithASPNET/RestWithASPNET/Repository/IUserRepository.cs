using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVo user);

        User? ValidateCredentials(string username);

        bool RevokeToken(string username);

        User? RefreshUserInfo(User user);
    }
}