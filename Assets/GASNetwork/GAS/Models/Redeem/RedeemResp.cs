using GAS.Common;
using Newtonsoft.Json;

namespace GAS.Models.Redeem
{
    public class RedeemData
    {
        /// <summary>
        /// 加密的兑换码内容
        /// </summary>
        [JsonProperty("content")] public string ContentEncrypted { get; set; }
    }

    public class RedeemResp : GASCommonResp<RedeemData> { }
}