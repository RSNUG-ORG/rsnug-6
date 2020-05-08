using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Escola.Dominio.Shared
{
    public sealed class EnderecoCompleto : ValueObject
    {
        public EnderecoCompleto(string rua, string numero, string complemento, string bairro, string cidade, string cep, string uF, string pais)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            UF = uF;
            Pais = pais;
        }

        public string Rua { get; }
        public string Numero { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Cep { get; }
        public string UF { get; }
        public string Pais { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rua;
            yield return Numero;
            yield return Complemento;
            yield return Bairro;
            yield return Cidade;
            yield return Cep;
            yield return UF;
            yield return Pais;
        }
    }
}
