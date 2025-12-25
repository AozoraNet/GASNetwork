using Newtonsoft.Json;

namespace GAS.Models.OAuth
{
    #region OAuth type=1  获取 auth_token
    public class OAuthAuthTokenReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 应用授权令牌
        /// </summary>
        [JsonProperty("apptoken")] public string AppTokenEncrypted { get; set; }
    }
    #endregion

    #region OAuth type=4  获取 access_token
    public class OAuthExchangeReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 操作类型1获取的授权令牌
        /// </summary>
        [JsonProperty("auth_token")] public string AuthToken { get; set; }
    }
    #endregion

    #region OAuth type=5  退出登录
    public class OAuthLogoutReq
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
        /// 访问令牌
        /// </summary>
        [JsonProperty("access_token")] public string AccessToken { get; set; }
    }
    #endregion
}