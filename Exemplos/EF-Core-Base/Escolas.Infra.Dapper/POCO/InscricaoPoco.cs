using System;

namespace Escolas.Infra.Dapper.POCO
{
    public class InscricaoPoco
    {
        public long TurmaId { get; set; }
        public DateTime InscritoEm { get; set; }
        public DateTime EncerraEm { get; set; }
        public string Situacao { get; set; }
        public long Id { get; internal set; }
    }
}