using System;
using System.Collections.Generic;
using System.Linq;

namespace PositivaMvc.Models
{
    public class GrupoPerguntas
    {
        public int Id { get; set; }
        public string Habilidade { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int AvaliacaoId { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; } = new List<Pergunta>();

        public GrupoPerguntas()
        {
        }

        public GrupoPerguntas(int id, string habilidade, Avaliacao avaliacao)
        {
            Id = id;
            Habilidade = habilidade;
            Avaliacao = avaliacao;
        }

        public void AddPergunta(Pergunta pergunta)
        {
            Perguntas.Add(pergunta);
        }

        public void RemovePergunta(Pergunta pergunta)
        {
            Perguntas.Remove(pergunta);
        }
    }
}