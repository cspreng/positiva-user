using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models.ViewModels
{
    public class AvaliacaoFormViewModel
    {
        public Avaliacao Avaliacao { get; set; }
        public ICollection<Colaborador> Colaboradors { get; set; }
        public List<Colaborador> Avaliacaos { get; internal set; }
    }
}
