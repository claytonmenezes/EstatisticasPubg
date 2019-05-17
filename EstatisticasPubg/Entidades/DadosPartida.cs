using System;
using System.Collections.Generic;

namespace EstatisticasPubg.Entidades
{
    public class DadosPartida
    {
        public List<JogadoresPartida> included { get; set; }
        public JogadoresPartidaData data { get; set; }
    }
    public class JogadoresPartida
    {
        public string id { get; set; }
        public AtributosPartida attributes { get; set; }
    }
    public class AtributosPartida
    {
        public StatusPartida stats { get; set; }
    }
    public class StatusPartida
    {
        public int kills { get; set; }
        public string playerId { get; set; }
    }
    public class JogadoresPartidaData
    {
        public AtributosPartidaData attributes { get; set; }

    }
    public class AtributosPartidaData
    {
        public DateTime createdAt { get; set; }
        public string gameMode { get; set; }
        public string mapName { get; set; }
    }
}