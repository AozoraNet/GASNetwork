using Newtonsoft.Json;

namespace GAS.Models.Version
{
    
    public class VersionReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
        
        /// <summary>
        /// 经过加密的序列号字符串
        /// </summary>
        [JsonProperty("sequence")] public string SequenceEncrypted { get; set; }
    }
}