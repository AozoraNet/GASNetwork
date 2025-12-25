using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.OAuth;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// GAS 开放授权登录接口
    /// </summary>
    public class OAuthService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// type=1  获取 auth_token
        /// </summary>
        /// <returns>OAuthAuthTokenResp</returns>
        public async UniTask<OAuthAuthTokenResp> GetAuthTokenAsync()
        {
            var encrypted = GASEncryption.Encrypt(GASConfigManager.AppToken, GASConfigManager.AppToken);

            var sendReq = new OAuthAuthTokenReq
            {
                AppId = GASConfigManager.AppId,
                AppTokenEncrypted = encrypted
            };

            string url = GASApiRoute.Endpoints.Oauth;
            var resp = await _http.PostAsync<OAuthAuthTokenResp>(url, sendReq, 1);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// type=4  通过 auth_token 获取 access_token
        /// </summary>
        /// <param name="authToken">应用授权令牌</param>
        /// <returns>OAuthAccessResp</returns>
        public async UniTask<OAuthAccessResp> ExchangeAuthTokenAsync(string authToken)
        {
            var sendReq = new OAuthExchangeReq
            {
                AppId = GASConfigManager.AppId,
                AuthToken = authToken
            };
            
            string url = GASApiRoute.Endpoints.Oauth;
            var resp = await _http.PostAsync<OAuthAccessResp>(url, sendReq, 4);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// type=5  退出登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <returns>OAuthLogoutResp</returns>
        public async UniTask<OAuthLogoutResp> LogoutAsync(string email, string accessToken)
        {
            var sendReq = new OAuthLogoutReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            string url = GASApiRoute.Endpoints.Oauth;
            var resp = await _http.PostAsync<OAuthLogoutResp>(url, sendReq, 5);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
    }
}