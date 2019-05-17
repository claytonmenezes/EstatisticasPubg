using System;

namespace EstatisticasPubg.Entidades
{
    public class Jogador
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Partidas { get; set; }
        public int Kills { get; set; }
    }
}