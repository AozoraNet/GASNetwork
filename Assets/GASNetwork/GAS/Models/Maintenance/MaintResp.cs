using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Maintenance
{
    /// <summary>
    /// 维护信息数据
    /// </summary>
    public class MaintData
    {
        /// <summary>
        /// 维护内容（加密）
        /// </summary>
        [JsonProperty("content")] public string ContentEncrypted { get; set; }
        
        /// <summary>
        /// 维护结束时间（加密，可能不存在）
        /// </summary>
        [JsonProperty("end_time")] public string EndTimeEncrypted { get; set; }
        
        /// <summary>
        /// 是否允许登录
        /// </summary>
        [JsonProperty("allow_login")] public bool AllowLogin { get; set; }
    }

    /// <summary>
    /// 应用维护信息获取接口响应
    /// code = 200: 查询成功，有维护事件
    /// code = 201: 查询成功，无维护事件或不在维护时间内
    /// </summary>
    public class MaintResp : GASCommonResp<MaintData>
    {
    }
}
