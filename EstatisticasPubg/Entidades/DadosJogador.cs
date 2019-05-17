using System.Collections.Generic;

namespace EstatisticasPubg.Entidades
{
    public class DadosJogador
    {
        public List<Dados> data { get; set; }
    }
    public class Dados
    {
        public string id { get; set; }
        public AtributoJogadora attributes { get; set; }
        public Relacionados relationships { get; set; }
    }
    public class AtributoJogadora
    {
        public string name { get; set; }
    }
    public class Relacionados
    {
        public Partidas matches { get; set; }
    }
    public class Partidas
    {
        public List<Dados> data { get; set; }
    }
}