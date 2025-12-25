using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Profile
{
    public class ProfileData
    {
        /// <summary>
        /// 用户唯一标识符
        /// </summary>
        [JsonProperty("uid")] public long Uid { get; set; }
        
        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonProperty("nickname")] public string Nickname { get; set; }
        
        /// <summary>
        /// 用户头像URL
        /// </summary>
        [JsonProperty("avatar")] public string Avatar { get; set; }
        
        /// <summary>
        /// 用户所在国家代码（基于IP）
        /// </summary>
        [JsonProperty("location")] public string Location { get; set; }
        
        /// <summary>
        /// 用户组别信息，逗号分隔
        /// </summary>
        [JsonProperty("user_group")] public string UserGroup { get; set; }
    }

    public class ProfileResp : GASCommonResp<ProfileData> { }
}