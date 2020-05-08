﻿namespace DDDTalk.Dominio.Turmas
{
    public sealed class Turma
    {
        internal Turma(string id, string descricao, int limiteIdade, int limiteAlunos, int totalInscritos, int duracaoEmMeses, decimal valorMensal)
        {
            Id = id;
            Descricao = descricao;
            LimiteIdade = limiteIdade;
            LimiteAlunos = limiteAlunos;
            TotalInscritos = totalInscritos;
            DuracaoEmMeses = duracaoEmMeses;
            ValorMensal = valorMensal;
        }

        public string Id { get; }
        public string Descricao { get; }
        public int LimiteIdade { get; }
        public int LimiteAlunos { get; }
        public int TotalInscritos { get; private set; }
        public int DuracaoEmMeses { get; }
        public decimal ValorMensal { get; }
        public int VagasDisponiveis => LimiteAlunos - TotalInscritos;

        public void IncrementarInscricao()
        {
            TotalInscritos++;
        }

        //public static Resultado<Turma, Falha> Nova(string descricao, int limiteIdade, int limiteAlunos, decimal valorMensal)
        //{
        //    if (String.IsNullOrEmpty(descricao) || (descricao.Length <= 5 && descricao.Length > 100))
        //        return Falha.Nova(400, "Descrição deve ter 5 a 100 letras");
        //    if(limiteAlunos <=0 || limiteAlunos > 100)
        //        return Falha.Nova(400, "Limite de alunos deve ser entre 1 e 99");
        //    return new Turma(Guid.NewGuid().ToString(), descricao, limiteIdade, limiteAlunos, 0, 12, valorMensal);
        //}
    }
}
