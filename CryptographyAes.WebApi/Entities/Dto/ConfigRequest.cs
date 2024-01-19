using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CryptographyAes.WebApi.Entities.Dto;
public class ConfigAesRequest
{
    [FromHeader]
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string key { get; set; }

    [FromHeader]
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string iv { get; set; }
}

public class ConfigGcmRequest
{
    [FromHeader]
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string key { get; set; }

    [FromHeader]
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string nonce { get; set; }
}
