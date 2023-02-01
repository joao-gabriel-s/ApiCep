using System.Dynamic;
using System.Net;

namespace ApiCep.Dtos
{
    public class Response<T> where T : class
    {
        public HttpStatusCode Http { get; set; }
        public T? Retorno { get; set; }
        public ExpandoObject? RespostaErro { get; set; }
    }
}
