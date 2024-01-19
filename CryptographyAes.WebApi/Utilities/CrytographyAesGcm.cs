using System.Text;
using System.Security.Cryptography;
using CryptographyAes.WebApi.Entities.Dto;

namespace CryptographyAes.WebApi.Utilities.Cryptography
{
    public class CryptographyAesGcm
    {

        //Método que genera la instancia del AesGcm
        // se actualiza el valor de la variable Nonce y se define la key que se sera utilizada en el encriptado y desencriptado.
        // la Key y el Nonce son unicas para cada llamado, si se cambia alguna de las 2 en el proceso de encriptado o desencriptado ya no será correcto.
        //      Key: para verificacion de 256 bits se debe crear una key de 32 bytes. Mismo uso que en la Key del Aes pero esta es solo asignada al momento de crear la instancia del AesGcm
        //      Nonce: debe ser de 12 bytes o 96 bits. Mismo uso que el IV del Aes pero este no es asignado como variable en la instancia solo se utiliza como parametro al momento de llamar los métodos.
        private static AesGcm CreateAESGCM(string secretKey)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey); //UTF8-Encoding
            var aesGcm = new AesGcm(key);
            return aesGcm;
        }

        // Método para recibir los parametros y realizar el proceso de encriptado
        // Parámetros: 
        //      dataToEnCrypt: el texto plano que se va a encriptar en array de bytes.
        //      associatedData (uuid): valor opcional utilizado como metadatos si es utilizado al momento de encriptar, debe proporcionarse al momento de desencriptar
        //  Variables:
        //      Tag: es un array que se genera durante el proceso de encriptado y es utilizado como llave de verificación para desencriptar.
        //      cipherText: variable que almacena el texto encriptado, debe ser de la misma longitud de bytes que el dato a encriptar
        public static (byte[], byte[]) GcmEncrypt(ConfigGcmRequest config, byte[] dataToEncrypt)
        {
            byte[] tag = new byte[16];
            byte[] cipherText = new byte[dataToEncrypt.Length];

            using (AesGcm aesGcm = CreateAESGCM(config.key))   //se invoca el metodo que asigna la instancia del AesGcm para tener la key a utilizar y tener el Nonce asociado a la key.
            {
                //Variable estatica para almacenar el valor del Nonce que se utiliza para el encriptado y desencriptado
                byte[] nonce = Encoding.UTF8.GetBytes(config.nonce);
                aesGcm.Encrypt(nonce, dataToEncrypt, cipherText, tag, null); //Método propio de la clase AesGcm para realizar el proceso de encriptado
            }

            return (cipherText, tag);  //Se retorna el texto encriptado y el tag de verificacion generado
        }

        // Método para recibir los parametros y realizar el proceso de desencriptado
        // Parámetros: 
        //      cipherText: el texto cifrado que se va a desencriptar en array de bytes.
        //      Tag: valor de verificacion utilizado en el proceso de encriptado para el cipherText en especifico.
        //      associatedData (uuid): valor opcional utilizado como metadatos si es utilizado al momento de encriptar, debe proporcionarse al momento de desencriptar
        // Variables:
        //      decryptedData: variable que almacena el texto desencriptado, debe ser de la misma longitud de bytes que el dato a encriptado
        public static byte[] GcmDecrypt(ConfigGcmRequest config, byte[] cipherText, byte[] tag)
        {
            byte[] decryptedData = new byte[cipherText.Length];

            using (AesGcm aesGcm = CreateAESGCM(config.key)) //se invoca el método que asigna la instancia del AesGcm para tener la key a utilizar y tener el Nonce asociado a la key.
            {
                byte[] nonce = Encoding.UTF8.GetBytes(config.nonce);
                aesGcm.Decrypt(nonce, cipherText, tag, decryptedData, null); //Método propio de la clase AesGcm para realizar el proceso de desencriptado
            }
            return decryptedData; //Se retorna el texto desencriptado en array de bytes
        }

    }
}

