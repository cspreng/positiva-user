using System;
using System.Collections.Generic;
using System.Linq;

namespace PositivaMvc.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public Colaborador Colaborador { get; set; }
        public int ColaboradorId { get; set; }
        public ICollection<GrupoPerguntas> GrupoPerguntass { get; set; } = new List<GrupoPerguntas>();

        public Avaliacao()
        {
        }

        public Avaliacao(int id, string feedback, Colaborador colaborador)
        {
            Id = id;
            Feedback = feedback;
            Colaborador = colaborador;
        }

        public void AddGrupoPerguntas(GrupoPerguntas grupoPerguntas)
        {
            GrupoPerguntass.Add(grupoPerguntas);
        }

        public void RemoveGrupoPerguntas(GrupoPerguntas grupoPerguntas)
        {
            GrupoPerguntass.Remove(grupoPerguntas);
        }
    }
}
