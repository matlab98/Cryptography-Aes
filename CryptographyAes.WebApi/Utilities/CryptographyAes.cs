using CryptographyAes.WebApi.Entities.Dto;
using CryptographyAes.WebApi.Entities.Exceptions;
using System.Security.Cryptography;

namespace CryptographyAes.WebApi.Utilities.Cryptography;

public class CryptographyAes
{
    private static AesManaged CreateAes(ConfigAesRequest config)
    {
        var aes = new AesManaged();
        aes.Key = System.Text.Encoding.UTF8.GetBytes(config.key); //UTF8-Encoding
        aes.IV = System.Text.Encoding.UTF8.GetBytes(config.iv);//UT8-Encoding
        return aes;
    }

    public static string AesEncrypt(ConfigAesRequest config, string text)
    {
        try
        {
            using (AesManaged aes = CreateAes(config))
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new XException("Error de servidor.");
        }
    }

    public static string AesDecrypt(ConfigAesRequest config, string text)
    {
        try
        {
            using (var aes = CreateAes(config))
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw new XException("Error de servidor.");
        }
    }
}

