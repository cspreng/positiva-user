using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public int Resultado { get; set; }
        public Avaliacao Avaliacaoa { get; set; }

        public Nota()
        {

        }

        public Nota(int id, int resultado, Avaliacao avaliacaoa)
        {
            Id = id;
            Resultado = resultado;
            Avaliacaoa = avaliacaoa;
        }
    }
}
