using System;

namespace Escola.API.ViewModel
{
    public class NovoAlunoInputModel
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}
