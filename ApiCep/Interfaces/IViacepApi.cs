using ApiCep.Dtos;
using ApiCep.Models;

namespace ApiCep.Interfaces
{
    public interface IViacepApi
    {
        Task<Response<EnderecoModel>> BuscarEnderecoCep(string cep);
    }
}
