using System;
using System.Collections.Generic;
using System.Linq;

namespace PositivaMvc.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string NomeColaborador { get; set; }
        public Empresa Empresa { get; set; }
        public int EmpresaId { get; set; }
        public Login Login { get; set; }
        public int? LoginId { get; set; }
        
        public ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

        public Colaborador()
        {
        }

        public Colaborador(int id, string nomeColaborador, Empresa empresa, Login login)
        {
            Id = id;
            NomeColaborador = nomeColaborador;
            Empresa = empresa;
            Login = login;
        }

        public void AddAvaliacao(Avaliacao avaliacao)
        {
            Avaliacaos.Add(avaliacao);
        }

        public void RemoveAvaliacao(Avaliacao avaliacao)
        {
            Avaliacaos.Remove(avaliacao);
        }
    }
}
