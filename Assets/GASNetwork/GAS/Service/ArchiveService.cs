using Cysharp.Threading.Tasks;
using GAS.Common;
using GAS.Config;
using GAS.Models.Archive;
using GAS.Network;

namespace GAS.Service
{
    /// <summary>
    /// GAS 云存档接口
    /// </summary>
    public class ArchiveService
    {
        private readonly GASHttpClient _http = new GASHttpClient();

        /// <summary>
        /// type=1 读取存档（返回解密后的内容）
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <returns>ArchiveReadResp</returns>
        public async UniTask<ArchiveReadResp> ReadAsync(string email, string accessToken)
        {
            var sendReq = new ArchiveReadReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            string url = GASApiRoute.Endpoints.Archive;
            var resp = await _http.PostAsync<ArchiveReadResp>(url, sendReq, 1);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// type=2 保存存档
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <param name="version">版本号</param>
        /// <param name="plainContent">存档内容（JSON）</param>
        /// <returns>ArchiveSaveResp</returns>
        public async UniTask<ArchiveSaveResp> SaveAsync(string email, string accessToken, string version, string plainContent)
        {
            // 加密明文 content
            string encryptedContent = GASEncryption.Encrypt(plainContent, GASConfigManager.AppToken);
            string encryptedVersion = GASEncryption.Encrypt(version, GASConfigManager.AppToken);

            var sendReq = new ArchiveSaveReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken,
                AppVersionEncrypted = encryptedVersion,
                ContentEncrypted = encryptedContent
            };

            string url = GASApiRoute.Endpoints.Archive;
            var resp = await _http.PostAsync<ArchiveSaveResp>(url, sendReq, 2);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// type=3 删除存档
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="accessToken">授权令牌</param>
        /// <returns>ArchiveDeleteResp</returns>
        public async UniTask<ArchiveDeleteResp> DeleteAsync(string email, string accessToken)
        {
            var sendReq = new ArchiveDeleteReq
            {
                AppId = GASConfigManager.AppId,
                Email = email,
                AccessToken = accessToken
            };
            
            string url = GASApiRoute.Endpoints.Archive;
            var resp = await _http.PostAsync<ArchiveDeleteResp>(url, sendReq, 3);
            GASResponseChecker.EnsureSuccess(resp);
            return resp;
        }

        /// <summary>
        /// 解密存档内容
        /// </summary>
        /// <param name="encryptedContent">加密存档内容</param>
        /// <returns>解密的存档</returns>
        public string DecryptArchiveContent(string encryptedContent)
        {
            return GASEncryption.Decrypt(encryptedContent, GASConfigManager.AppToken);
        }
    }
}