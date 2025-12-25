using Newtonsoft.Json;

namespace GAS.Models.Archive
{
    public class ArchiveBaseReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [JsonProperty("email")] public string Email { get; set; }
        
        /// <summary>
        /// 加密后的访问令牌
        /// </summary>
        [JsonProperty("access_token")] public string AccessToken { get; set; }
    }

    #region type=1 Read
    public class ArchiveReadReq : ArchiveBaseReq { }
    #endregion

    #region type=2 Save
    public class ArchiveSaveReq : ArchiveBaseReq
    {
        /// <summary>
        /// 加密后的应用版本号
        /// </summary>
        [JsonProperty("app_version")] public string AppVersionEncrypted { get; set; }
        
        /// <summary>
        /// 加密后的存档内容
        /// </summary>
        [JsonProperty("content")] public string ContentEncrypted { get; set; }
    }
    #endregion

    #region type=3 Delete
    public class ArchiveDeleteReq : ArchiveBaseReq { }
    #endregion
}