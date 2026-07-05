using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Ban;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// GAS 应用层面封禁用户接口
    /// </summary>
    public class BanService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 封禁用户，可设置封禁原因和时长
        /// </summary>
        /// <param name="uid">用户唯一标识符（未加密）</param>
        /// <param name="reason">封禁原因（未加密，可选）</param>
        /// <param name="banTimeHours">封禁时长，单位：小时（可选，不提供则永久封禁）</param>
        /// <returns>BanResp</returns>
        public async UniTask<BanResp> BanUserAsync(string uid, string reason = null, int? banTimeHours = null)
        {
            var encryptedUid = GASEncryption.Encrypt(uid, GASConfigManager.AppToken);
            
            var sendReq = new BanReq
            {
                AppId = GASConfigManager.AppId,
                UidEncrypted = encryptedUid
            };

            // 如果提供了封禁原因，进行加密
            if (!string.IsNullOrEmpty(reason))
            {
                sendReq.ReasonEncrypted = GASEncryption.Encrypt(reason, GASConfigManager.AppToken);
            }

            // 如果提供了封禁时长，进行加密
            if (banTimeHours.HasValue)
            {
                sendReq.BanTimeEncrypted = GASEncryption.Encrypt(banTimeHours.Value.ToString(), GASConfigManager.AppToken);
            }

            var resp = await _http.PostAsync<BanResp>(GASApiRoute.Endpoints.Ban, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// 永久封禁用户
        /// </summary>
        /// <param name="uid">用户唯一标识符（未加密）</param>
        /// <param name="reason">封禁原因（未加密，可选）</param>
        /// <returns>BanResp</returns>
        public async UniTask<BanResp> BanUserPermanentlyAsync(string uid, string reason = null)
        {
            return await BanUserAsync(uid, reason, null);
        }

        /// <summary>
        /// 临时封禁用户
        /// </summary>
        /// <param name="uid">用户唯一标识符（未加密）</param>
        /// <param name="banTimeHours">封禁时长，单位：小时</param>
        /// <param name="reason">封禁原因（未加密，可选）</param>
        /// <returns>BanResp</returns>
        public async UniTask<BanResp> BanUserTemporarilyAsync(string uid, int banTimeHours, string reason = null)
        {
            if (banTimeHours <= 0)
            {
                throw new GASException("Ban time must be greater than 0");
            }

            return await BanUserAsync(uid, reason, banTimeHours);
        }
    }
}
