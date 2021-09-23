using System.Collections.Generic;

namespace PositivaMvc.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string CadastroEmpresa { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();

        public Empresa()
        {
        }

        public Empresa(int id, string cadastroEmpresa)
        {
            Id = id;
            CadastroEmpresa = cadastroEmpresa;
        }

        public void AddColaborador(Colaborador colaborador)
        {
            Colaboradores.Add(colaborador);
        }

    public void RemoveColaborador(Colaborador colaborador)
        {
            Colaboradores.Remove(colaborador);
        }
    }


}
