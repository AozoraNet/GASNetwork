using System.Collections.Generic;
using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Version
{
    public class VersionData
    {
        /// <summary>
        /// 版本号数组
        /// </summary>
        [JsonProperty("version")] public List<string> Versions { get; set; }
    }

    public class VersionResp : GASCommonResp<VersionData> { }
}