using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Archive
{
    #region Read
    public class ArchiveReadData
    {
        /// <summary>
        /// 加密后的存档内容
        /// </summary>
        [JsonProperty("content")] public string ContentEncrypted { get; set; }
        
        /// <summary>
        /// 存档对应的应用版本
        /// </summary>
        [JsonProperty("app_version")] public string AppVersion { get; set; }
        
        /// <summary>
        /// 存档更新时间
        /// </summary>
        [JsonProperty("update_time")] public string UpdateTime { get; set; }
    }

    public class ArchiveReadResp : GASCommonResp<ArchiveReadData> { }
    #endregion

    #region Save / Delete 
    public class ArchiveSaveResp : GASCommonResp<object> { }
    public class ArchiveDeleteResp : GASCommonResp<object> { }
    #endregion
}