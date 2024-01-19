using CryptographyAes.WebApi.Entities.Dto;
using System.Net;

namespace CryptographyAes.WebApi.Business;
public interface IAesService
{
    Task<DefaultResponse> AesEncrypt(ConfigAesRequest config, AesEncryptRequest request);

    Task<DefaultResponse> AesDecrypt(ConfigAesRequest config, AesDecryptRequest request);

    Task<DefaultResponse> AesGcmEncrypt(ConfigGcmRequest config, GcmEncryptRequest request);

    Task<DefaultResponse> AesGcmDecrypt(ConfigGcmRequest config, GcmDecryptRequest request);
}

