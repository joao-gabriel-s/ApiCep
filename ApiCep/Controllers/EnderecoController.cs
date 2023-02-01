using ApiCep.Dtos;
using ApiCep.Interfaces;
using ApiCep.Models;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiCep.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        public readonly ICepService _cepService;
        public readonly IMemoryCache _memoryCache;

        public EnderecoController(ICepService enderecoService,
                                  IMemoryCache memoryCache)
        {
            _cepService = enderecoService;
            _memoryCache = memoryCache;
        }


        [HttpGet("busca/{cep}"),Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarDados([FromRoute] string cep)
        {
            if (_memoryCache.TryGetValue(cep, out EnderecoResponse infos))
            {
                return Ok(infos);
            }

            cep = cep.Replace(".", "");
            cep = cep.Replace("-", "");
            cep = cep.Replace(" ", "");

            Regex Rgx = new Regex(@"^\d{8}$");

            if (!Rgx.IsMatch(cep))
                return Ok("CEP inválido");

            var response = await _cepService.BuscarCep(cep);

            if (response.Http == 0)
                return Ok("CEP inválido");

            if (response.Http == HttpStatusCode.OK)
            {
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };

                _memoryCache.Set(cep, response.Retorno, memoryCacheEntryOptions);

                return Ok(response.Retorno);
            }
            else
            {
                return StatusCode((int)response.Http, response.RespostaErro);
            }
        }

    }
}
