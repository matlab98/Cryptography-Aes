using CryptographyAes.WebApi.Entities.Dto;
using System.Text;

namespace CryptographyAes.WebApi.Business.ApplicationService;

public class AesService : IAesService
{
    public AesService()
    { }

    public async Task<DefaultResponse> AesEncrypt(ConfigAesRequest config, AesEncryptRequest request)
    {
        try
        {
            string decryptedValue = CryptographyAes.WebApi.Utilities.Cryptography.CryptographyAes.AesEncrypt(config, request.toEncrypt);

            return new DefaultResponse()
            {
                status = true,
                statusDescription = "Se realiza la encriptación.",
                data = new {
                    encryptedData = decryptedValue
                }
            };

        }
        catch (Exception ex)
        {
            return new DefaultResponse()
            {
                status = false,
                statusDescription = "Error en el proceso de encriptación.",
                data = new { error = ex.Message }
            };

        }
    }

    public async Task<DefaultResponse> AesDecrypt(ConfigAesRequest config, AesDecryptRequest request)
    {
        try
        {
            string decryptedValue = CryptographyAes.WebApi.Utilities.Cryptography.CryptographyAes.AesDecrypt(config, request.encryptedText);

            return new DefaultResponse()
            {
                status = true,
                statusDescription = "Se realiza la encriptación.",
                data = new {
                    encryptedData = decryptedValue
                }
            };

        }
        catch (Exception ex)
        {
            return new DefaultResponse()
            {
                status = false,
                statusDescription = "Error en el proceso de encriptación.",
                data = new { error = ex.Message }
            };
        }
    }

    public async Task<DefaultResponse> AesGcmDecrypt(ConfigGcmRequest config, GcmDecryptRequest request)
    {
        try
        {
            var decryptedValue = CryptographyAes.WebApi.Utilities.Cryptography.CryptographyAesGcm.GcmDecrypt(config, Convert.FromBase64String(request.key), Convert.FromBase64String(request.tag));
            string KeyPass = Encoding.UTF8.GetString(decryptedValue);
            return new DefaultResponse()
            {
                status = true,
                statusDescription = "Se realiza la desencriptación.",
                data = new {
                    decryptedData = KeyPass
                }
            };

        }
        catch (Exception ex)
        {
            return new DefaultResponse()
            {
                status = false,
                statusDescription = "Error en el proceso de desencriptación.",
                data = new { error = ex.Message }
            };
        }
    }

    public async Task<DefaultResponse> AesGcmEncrypt(ConfigGcmRequest config, GcmEncryptRequest request)
    {
        try
        {
            (byte[] cipherText, byte[] tag) encryptedValue = CryptographyAes.WebApi.Utilities.Cryptography.CryptographyAesGcm.GcmEncrypt(config, Encoding.UTF8.GetBytes(request.data));

            string cipherText = Convert.ToBase64String(encryptedValue.cipherText);
            string tag = Convert.ToBase64String(encryptedValue.tag);

            return new DefaultResponse()
            {
                status = true,
                statusDescription = "Se realiza la encriptación.",
                data = new {
                    data = cipherText,
                    tag = tag
                }
            };

        }
        catch (Exception ex)
        {
            return new DefaultResponse()
            {
                status = false,
                statusDescription = "Error en el proceso de encriptación.",
                data = new { error = ex.Message }
            };
        }
    }
}

