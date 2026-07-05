using Newtonsoft.Json;

namespace GAS.Models.Ban
{
    /// <summary>
    /// 应用层面封禁用户接口请求
    /// </summary>
    public class BanReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 用户唯一标识符（加密）
        /// </summary>
        [JsonProperty("uid")] public string UidEncrypted { get; set; }
        
        /// <summary>
        /// 封禁原因（加密，可选）
        /// </summary>
        [JsonProperty("reason")] public string ReasonEncrypted { get; set; }
        
        /// <summary>
        /// 封禁时长，单位：小时（加密，可选）
        /// 不提供则表示永久封禁
        /// </summary>
        [JsonProperty("banTime")] public string BanTimeEncrypted { get; set; }
    }
}
