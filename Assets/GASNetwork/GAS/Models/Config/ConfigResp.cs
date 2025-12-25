using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Config
{
    public class ConfigData
    {
        /// <summary>
        /// 加密的配置内容
        /// </summary>
        [JsonProperty("config")] public string Config { get; set; }
    }
    
    public class ConfigResp: GASCommonResp<ConfigData> { }
}