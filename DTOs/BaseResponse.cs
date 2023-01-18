using Newtonsoft.Json;

namespace DTOs;

public class BaseResponse<T> where T : class
{
    [JsonProperty("total_elements")]
    public int TotalElements { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }

    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("content")]
    public T Content {get;set; }
}