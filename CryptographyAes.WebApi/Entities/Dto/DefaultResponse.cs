using Newtonsoft.Json;
using System.ComponentModel;

namespace CryptographyAes.WebApi.Entities.Dto;

public class DefaultResponse
{
    [DefaultValue(true)]
    public bool status { get; set; }

    public string statusDescription { get; set; }

    [JsonIgnore]
    public dynamic data { get; set; }
}



