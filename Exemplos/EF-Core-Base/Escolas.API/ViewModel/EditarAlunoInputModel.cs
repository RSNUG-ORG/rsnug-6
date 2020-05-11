using System;

namespace Escolas.API.ViewModel
{
    public class EditarAlunoInputModel
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
