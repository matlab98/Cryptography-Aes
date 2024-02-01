using CryptographyAes.WebApi.Business;
using CryptographyAes.WebApi.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CryptographyAes.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CryptographyController : ControllerBase
{
    /// <summary>
    ///instancia de la interfaz para llamar los Request 
    /// </summary>
    private readonly IAesService _aesService;

    public CryptographyController(IAesService aesService)
    {
        _aesService = aesService;
    }

    [HttpPost("v1_0/AesEncrypt", Name = "inSafe")]
    [HttpPost("v1_0/AesSafeEncrypt", Name = "safe")]
    public async Task<IResult> AesEncrypt([FromHeader] ConfigAesRequest config, [FromBody] AesEncryptRequest request)
    {
        try
        {
            string endpoint = ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;

            Console.WriteLine(JsonConvert.SerializeObject(request));

            DefaultResponse response;
            if (endpoint == "inSafe")
            {
                response = await _aesService.AesEncrypt(config, request);
            }
            else
            {
                response = await _aesService.AesSafeEncrypt(config, request);
            }

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("v1_0/AesDecrypt", Name = "inSafe")]
    [HttpPost("v1_0/AesSafeEncrypt", Name = "safe")]
    public async Task<IResult> AesDecrypt([FromHeader] ConfigAesRequest config, [FromBody] AesDecryptRequest request)
    {
        try
        {
            string endpoint = ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;

            DefaultResponse response;
            var response = await _aesService.AesDecrypt(config, request);
            if (endpoint == "inSafe")
            {
                response = await _aesService.AesDecrypt(config, request);
            }
            else
            {
                response = await _aesService.AesSafeDecrypt(config, request);
            }
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    [HttpPost("v1_0/AesGcmEncrypt")]
    public async Task<IResult> AesEncrypt([FromHeader] ConfigGcmRequest config, [FromBody] GcmEncryptRequest request)
    {
        try
        {
            var response = await _aesService.AesGcmEncrypt(config, request);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    [HttpPost("v1_0/AesGcmDecrypt")]
    public async Task<IResult> AesDecrypt([FromHeader] ConfigGcmRequest config, [FromBody] GcmDecryptRequest request)
    {
        try
        {
            var response = await _aesService.AesGcmDecrypt(config, request);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
