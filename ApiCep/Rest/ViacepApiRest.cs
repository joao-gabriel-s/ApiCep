using ApiCep.Dtos;
using ApiCep.Interfaces;
using ApiCep.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json;

namespace ApiCep.Rest
{
    public class ViacepApiRest : IViacepApi
    {
        public async Task<Response<EnderecoModel>> BuscarEnderecoCep(string cep)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{cep}/json/");

            var response = new Response<EnderecoModel>();
            using (var client = new HttpClient())
            {
                var responseViacepApi = await client.SendAsync(request);
                var contentResp = await responseViacepApi.Content.ReadAsStringAsync();


                var objResponse = JsonSerializer.Deserialize<EnderecoModel>(contentResp);

                if (!responseViacepApi.IsSuccessStatusCode || objResponse.Cep == null)
                {
                    return response;

                }

                if (responseViacepApi.IsSuccessStatusCode)
                {
                    response.Http = responseViacepApi.StatusCode;
                    response.Retorno = objResponse;
                }
                else
                {
                    response.Http = responseViacepApi.StatusCode;
                    response.RespostaErro = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
                }
            }
            return response;
        }


    }
}
