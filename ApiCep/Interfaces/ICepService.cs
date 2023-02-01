using ApiCep.Dtos;

namespace ApiCep.Interfaces
{
    public interface ICepService
    {
        Task<Response<EnderecoResponse>> BuscarCep(string cep);     
    }
}
