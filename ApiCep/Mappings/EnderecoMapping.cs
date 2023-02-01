using AutoMapper;
using ApiCep.Dtos;
using ApiCep.Models;

namespace ApiCep.Mappings
{
    public class EnderecoMapping : Profile
    {
        public EnderecoMapping()
        {
            CreateMap(typeof(Response<>), typeof(Response<>));
            CreateMap<EnderecoResponse, EnderecoModel>();
            CreateMap<EnderecoModel, EnderecoResponse>();
        }
    }
}
