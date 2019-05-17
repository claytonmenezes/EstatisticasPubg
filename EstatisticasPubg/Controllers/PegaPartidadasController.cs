using EstatisticasPubg.Entidades;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Web.Http;

namespace EstatisticasPubg.Controllers
{
    public class PegaPartidadasController : ApiController
    {
        [HttpGet]
        public Jogador Listar(string nomeJogador)
        {
            var jogador = new Jogador();

            using (var webClientJogador = new WebClient())
            {
                webClientJogador.Headers.Add("Authorization", @"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJiNjliZGUzMC00ODMzLTAxMzctMGRkZi0yOTNiMmJkYTI4YmYiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTU2MDUxMDY2LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImZlbGlwcGUtZGUtZnJlIn0.zqGs68NPKwUBa3tVH5QDwrYTNDWGRLPjRufND-zqkYQ");
                webClientJogador.Headers.Add("accept", "application/json");
                webClientJogador.Encoding = Encoding.UTF8;
                var jsonJogadora = webClientJogador.DownloadString("https://api.pubg.com/shards/steam/players?filter[playerNames]=" + nomeJogador);
                var dadosApiJogador = JsonConvert.DeserializeObject<DadosJogador>(jsonJogadora);

                foreach (var dadoJogador in dadosApiJogador.data)
                {
                    bool fim = false;
                    jogador.Id = dadoJogador.id;
                    jogador.Nome = dadoJogador.attributes.name;
                    foreach (var match in dadoJogador.relationships.matches.data)
                    {
                        if (fim) break;
                        using (var webClientPartida = new WebClient())
                        {
                            webClientJogador.Headers.Add("Authorization", @"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJiNjliZGUzMC00ODMzLTAxMzctMGRkZi0yOTNiMmJkYTI4YmYiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTU2MDUxMDY2LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImZlbGlwcGUtZGUtZnJlIn0.zqGs68NPKwUBa3tVH5QDwrYTNDWGRLPjRufND-zqkYQ");
                            webClientJogador.Headers.Add("accept", "application/json");
                            webClientJogador.Encoding = Encoding.UTF8;
                            var jsonPartida = webClientJogador.DownloadString("https://api.pubg.com/shards/steam/matches/" + match.id);
                            var dadosApiPartida = JsonConvert.DeserializeObject<DadosPartida>(jsonPartida);

                            foreach (var dadoPartida in dadosApiPartida.included)
                            {
                                if (dadoPartida.attributes.stats != null && dadoPartida.attributes.stats.playerId == dadoJogador.id)
                                {
                                    jogador.Partidas += 1;
                                    jogador.Kills += dadoPartida.attributes.stats.kills;
                                }
                                if (dadosApiPartida.data.attributes.createdAt.Date < DateTime.Now.Date)
                                {
                                    fim = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return jogador;
        }
    }
}
