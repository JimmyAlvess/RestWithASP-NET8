using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVo ValidateCredentials(UserVo user);
        TokenVo ValidateCredentials(TokenVo token);
        bool RevokeToken(string userName);
    }
}
