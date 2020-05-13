using System;

namespace Escola.API.ViewModel
{
    public class NovaTurmaInputModel
    {        
        public string Descricao { get; set; }
        public int LimiteIdade { get; set; }
        public int QuantidadeMinimaAlunos { get; set; }
        public int QuantidadeMaximaAlunos { get; set; }
        public bool ComDuracao { get; set; }
        public string DuracaoTipo { get; set; }
        public int DuracaoValor { get; set; }
    }
}
