using Newtonsoft.Json;

namespace GAS.Common
{
    public class GASCommonResp<T>
    {
        [JsonProperty("code")] public int Code { get; set; }
        [JsonProperty("msg")] public string Msg { get; set; }
        [JsonProperty("data")] public T Data { get; set; }
    }
}