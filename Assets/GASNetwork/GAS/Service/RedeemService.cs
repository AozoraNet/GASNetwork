using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Redeem;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// 兑换码核销接口
    /// </summary>
    public class RedeemService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 单次兑换（可匿名）
        /// </summary>
        /// <param name="redeemCode">兑换码</param>
        /// <returns>RedeemResp</returns>
        public async UniTask<RedeemResp> RedeemAnonymousAsync(string redeemCode)
        {
            var sendReq = new RedeemAnonymousReq
            {
                AppId = GASConfigManager.AppId,
                RedeemCode = redeemCode
            };
            
            var resp = await _http.PostAsync<RedeemResp>(GASApiRoute.Endpoints.Redeem, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// 全局兑换（验证登录）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <param name="redeemCode">兑换码</param>
        /// <returns>RedeemResp</returns>
        public async UniTask<RedeemResp> RedeemWithAccountAsync(string email, string accessToken, string redeemCode)
        {
            var sendReq = new RedeemWithAccountReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken,
                RedeemCode = redeemCode
            };
            
            var resp = await _http.PostAsync<RedeemResp>(GASApiRoute.Endpoints.Redeem, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 全局兑换（验证登录/旧版）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="userToken">老版本用户令牌</param>
        /// <param name="redeemCode">兑换码</param>
        /// <returns>RedeemResp</returns>
        public async UniTask<RedeemResp> RedeemWithAccountAsyncOld(string email, string userToken, string redeemCode)
        {
            var sendReq = new RedeemWithAccountReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                UserToken = userToken,
                RedeemCode = redeemCode
            };
            
            var resp = await _http.PostAsync<RedeemResp>(GASApiRoute.Endpoints.Redeem, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// 解密兑换内容（contentEncrypted）
        /// </summary>
        /// <param name="encryptedContent">加密的兑换码内容</param>
        /// <returns>解密后是在后台设置的兑换内容</returns>
        public string DecryptRedeemContent(string encryptedContent)
        {
            return GASEncryption.Decrypt(encryptedContent, GASConfigManager.AppToken);
        }
    }
}