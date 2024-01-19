namespace CryptographyAes.WebApi.Entities.Dto;

public class GcmEncryptRequest
{
    public string data { get; set; }
}

public class GcmDecryptRequest
{
    public string key { get; set; }
    public string tag { get; set; }
}

