using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Profile;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// 获取用户信息接口
    /// </summary>
    public class ProfileService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 获取个人基本信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <returns>ProfileResp</returns>
        public async UniTask<ProfileResp> GetProfileAsync(string email, string accessToken)
        {
            var sendReq = new ProfileReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            var resp = await _http.PostAsync<ProfileResp>(GASApiRoute.Endpoints.Profile, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 获取个人基本信息（旧版）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="userToken">老版本用户令牌</param>
        /// <returns></returns>
        public async UniTask<ProfileResp> GetProfileAsyncOld(string email, string userToken)
        {
            var sendReq = new ProfileReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                UserToken = userToken
            };
            
            var resp = await _http.PostAsync<ProfileResp>(GASApiRoute.Endpoints.Profile, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
    }
}