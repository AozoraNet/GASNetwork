using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Config;
using GAS.Models.Version;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// 配置信息接口
    /// </summary>
    public class ConfigService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns>VersionResp</returns>
        public async UniTask<ConfigResp> GetConfigAsync()
        {
            var sendReq = new ConfigReq()
            {
                AppId = GASConfigManager.AppId
            };

            var resp = await _http.PostAsync<ConfigResp>(GASApiRoute.Endpoints.Config, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 解密配置信息（contentEncrypted）
        /// </summary>
        /// <param name="encryptedConfig">加密的配置信息</param>
        /// <returns>解密后是在后台设置的配置信息</returns>
        public string DecryptConfig(string encryptedConfig)
        {
            return GASEncryption.Decrypt(encryptedConfig, GASConfigManager.AppToken);
        }
    }
}