using Newtonsoft.Json;

namespace GAS.Models.Config
{
    public class ConfigReq
    {
        /// <summary>
        /// 应用唯一标识符
        /// </summary>
        [JsonProperty("appid")] public int AppId { get; set; }
    }
}