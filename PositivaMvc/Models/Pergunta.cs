using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models
{
    public class Pergunta
    {
        public int Id { get; set; }
        public String Carisma { get; set; }
        public GrupoPerguntas GrupoPerguntas { get; set; }
        public int GrupoPerguntasId { get; set; }

        public Pergunta()
        {

        }

        public Pergunta(int id, string carisma, GrupoPerguntas grupoPerguntas)
        {
            Id = id;
            Carisma = carisma;
            GrupoPerguntas = grupoPerguntas;
        }



    }
}
