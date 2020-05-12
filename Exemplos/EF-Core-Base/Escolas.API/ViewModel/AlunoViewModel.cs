using System;

namespace Escola.API.ViewModel
{
    public class AlunoViewModel
    {
        public AlunoViewModel(string nomeCompleto, string email, DateTime dataNascimento, string sexo)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}
