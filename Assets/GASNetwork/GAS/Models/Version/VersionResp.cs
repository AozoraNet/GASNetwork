using System.Collections.Generic;
using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Version
{
    public class VersionData
    {
        /// <summary>
        /// 加密版本号
        /// </summary>
        [JsonProperty("version")] public string Versions { get; set; }
    }

    public class VersionResp : GASCommonResp<VersionData> { }
}