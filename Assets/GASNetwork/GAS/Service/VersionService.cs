using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Version;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// 版本管理接口
    /// </summary>
    public class VersionService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// 获取最新版本号
        /// </summary>
        /// <param name="sequence">版本序列</param>
        /// <returns>VersionResp</returns>
        public async UniTask<VersionResp> GetVersionAsync(string sequence)
        {
            string encrypted = GASEncryption.Encrypt(sequence, GASConfigManager.AppToken);

            var sendReq = new VersionReq
            {
                AppId = GASConfigManager.AppId,
                SequenceEncrypted = encrypted
            };

            var resp = await _http.PostAsync<VersionResp>(GASApiRoute.Endpoints.Version, sendReq);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }
        
        /// <summary>
        /// 解密版本号
        /// </summary>
        /// <param name="encryptedContent">加密的版本号</param>
        /// <returns>解密后版本号</returns>
        public List<string> DecryptVersion(string encryptedContent)
        {
            var decryptContent = GASEncryption.Decrypt(encryptedContent, GASConfigManager.AppToken);
            if (string.IsNullOrEmpty(decryptContent))
            {
                return new List<string>();
            }
            return decryptContent
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .Where(v => v.Length > 0)
                .ToList();
        }
    }
}