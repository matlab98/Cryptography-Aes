﻿using System.ComponentModel.DataAnnotations;
namespace CryptographyAes.WebApi.Entities.Dto;

public class AesEncryptRequest
{
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string toEncrypt { get; set; }
}

public class AesDecryptRequest
{
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string encryptedText { get; set; }
}