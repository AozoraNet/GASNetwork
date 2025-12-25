using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.AutoLogin;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// GAS 自动登录接口
    /// </summary>
    public class AutoLoginService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 自动登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <returns>AutoLoginResp</returns>
        public async UniTask<AutoLoginResp> AutoLoginAsync(string email, string accessToken)
        {
            var sendReq = new AutoLoginReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            var resp = await _http.PostAsync<AutoLoginResp>(GASApiRoute.Endpoints.AutoLogin, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 自动登录（旧版）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="userToken">老版本访问令牌</param>
        /// <returns>AutoLoginResp</returns>
        public async UniTask<AutoLoginResp> AutoLoginAsyncOld(string email, string userToken)
        {
            var sendReq = new AutoLoginReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                UserToken = userToken
            };
            
            var resp = await _http.PostAsync<AutoLoginResp>(GASApiRoute.Endpoints.AutoLogin, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
    }
}