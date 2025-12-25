using Newtonsoft.Json;

namespace GAS.Models.Redeem
{
    #region 匿名兑换
    public class RedeemAnonymousReq
    {
        [JsonProperty("appid")] public int AppId { get; set; }
        [JsonProperty("redeem_code")] public string RedeemCode { get; set; }
    }
    #endregion

    #region 登录账号兑换
    public class RedeemWithAccountReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        [JsonProperty("email")] public string Email { get; set; }
        
        /// <summary>
        /// 老版本用户令牌（与access_token二选一）
        /// </summary>
        [JsonProperty("user_token")] public string UserToken { get; set; }
        
        /// <summary>
        /// 新版本访问令牌（与user_token二选一）
        /// </summary>
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        
        /// <summary>
        /// 兑换码
        /// </summary>
        [JsonProperty("redeem_code")] public string RedeemCode { get; set; }
    }
    #endregion
}