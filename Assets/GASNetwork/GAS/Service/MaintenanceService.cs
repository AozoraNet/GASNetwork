using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Maintenance;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// GAS 应用维护信息获取接口
    /// 查询应用的实时维护信息
    /// </summary>
    public class MaintenanceService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 获取应用维护信息
        /// </summary>
        /// <returns>MaintResp
        /// code = 200: 有维护事件，返回维护信息
        /// code = 201: 无维护事件或不在维护时间内
        /// </returns>
        public async UniTask<MaintResp> GetMaintenanceInfoAsync()
        {
            var encryptedToken = GASEncryption.Encrypt(GASConfigManager.AppToken, GASConfigManager.AppToken);

            var sendReq = new MaintReq
            {
                AppId = GASConfigManager.AppId,
                AppTokenEncrypted = encryptedToken
            };

            var resp = await _http.PostAsync<MaintResp>(GASApiRoute.Endpoints.Maintenance, sendReq);
            
            // 注意：code = 201 表示无维护事件，这是成功情况，不应抛出异常
            // 只在 code != 200 && code != 201 时才检查
            if (resp.Code != 200 && resp.Code != 201)
            {
                GASResponseChecker.EnsureSuccess(resp);
            }

            return resp;
        }

        /// <summary>
        /// 检查是否存在维护事件
        /// </summary>
        /// <returns>true 表示有维护事件，false 表示无维护事件</returns>
        public async UniTask<bool> HasMaintenanceAsync()
        {
            var resp = await GetMaintenanceInfoAsync();
            return resp.Code == 200 && resp.Data != null;
        }

        /// <summary>
        /// 获取解密后的维护内容
        /// </summary>
        /// <param name="encryptedContent">加密后的维护内容</param>
        /// <returns>解密后的维护内容</returns>
        public string DecryptMaintenanceContent(string encryptedContent)
        {
            if (string.IsNullOrEmpty(encryptedContent))
                return "";

            return GASEncryption.Decrypt(encryptedContent, GASConfigManager.AppToken);
        }

        /// <summary>
        /// 获取解密后的维护结束时间
        /// </summary>
        /// <param name="encryptedEndTime">加密后的结束时间</param>
        /// <returns>解密后的结束时间 (如 "2026-04-01 12:00:00")</returns>
        public string DecryptMaintenanceEndTime(string encryptedEndTime)
        {
            if (string.IsNullOrEmpty(encryptedEndTime))
                return "";

            return GASEncryption.Decrypt(encryptedEndTime, GASConfigManager.AppToken);
        }
    }
}
