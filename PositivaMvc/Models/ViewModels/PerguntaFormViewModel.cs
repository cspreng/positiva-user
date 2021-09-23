using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models.ViewModels
{
    public class PerguntaFormViewModel
    {
        public Pergunta Pergunta { get; set; }
        public ICollection<GrupoPerguntas> GrupoPerguntass { get; set; }
    }
}
