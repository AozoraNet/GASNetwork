using Newtonsoft.Json;

namespace GAS.Models.Maintenance
{
    /// <summary>
    /// 应用维护信息获取接口请求
    /// </summary>
    public class MaintReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 加密后的应用秘钥
        /// </summary>
        [JsonProperty("apptoken")] public string AppTokenEncrypted { get; set; }
    }
}
