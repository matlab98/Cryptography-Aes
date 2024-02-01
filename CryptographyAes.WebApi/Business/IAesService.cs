using CryptographyAes.WebApi.Entities.Dto;

namespace CryptographyAes.WebApi.Business;

public interface IAesService
{
    Task<DefaultResponse> AesEncrypt(ConfigAesRequest config, AesEncryptRequest request);
    Task<DefaultResponse> AesSafeEncrypt(ConfigAesRequest config, AesEncryptRequest request);
    Task<DefaultResponse> AesDecrypt(ConfigAesRequest config, AesDecryptRequest request);
    Task<DefaultResponse> AesSafeDecrypt(ConfigAesRequest config, AesDecryptRequest request);

    Task<DefaultResponse> AesGcmEncrypt(ConfigGcmRequest config, GcmEncryptRequest request);

    Task<DefaultResponse> AesGcmDecrypt(ConfigGcmRequest config, GcmDecryptRequest request);
}

