using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models.ViewModels
{
    public class GrupoPerguntasFormViewModel
    {
        public GrupoPerguntas GrupoPerguntas { get; set; }
        public ICollection<Avaliacao> Avaliacaos { get; set; }
    }
}
