using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.OAuth
{
    #region OAuth type=1 返回
    public class OAuthAuthTokenData
    {
        [JsonProperty("auth_token")] public string AuthToken { get; set; }
        [JsonProperty("expire")] public long? ExpireUnix { get; set; }
    }

    public class OAuthAuthTokenResp : GASCommonResp<OAuthAuthTokenData> { }
    #endregion


    #region OAuth type=4 返回 access_token
    public class OAuthAccessData
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("uid")] public long? Uid { get; set; }
        [JsonProperty("user_group")] public string UserGroup { get; set; }
    }

    public class OAuthAccessResp : GASCommonResp<OAuthAccessData> { }
    #endregion


    #region OAuth type=5 返回
    public class OAuthLogoutResp : GASCommonResp<object> { }
    #endregion
}