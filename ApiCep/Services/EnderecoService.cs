using AutoMapper;
using ApiCep.Dtos;
using ApiCep.Interfaces;

namespace ApiCep.Services
{
    public class EnderecoService : ICepService
    {
        private readonly IMapper _mapper;
        private readonly IViacepApi _viacepApi;

        public EnderecoService(IMapper mapper, IViacepApi viacepApi)
        {
            _mapper = mapper;
            _viacepApi = viacepApi;
        }

        public async Task<Response<EnderecoResponse>> BuscarCep(string cep)
        {
            var dadosEndereco = await _viacepApi.BuscarEnderecoCep(cep);

            return _mapper.Map<Response<EnderecoResponse>>(dadosEndereco);
        }
       
    }
}
