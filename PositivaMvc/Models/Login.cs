using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PositivaMvc.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Usuario { get; set; }        
        public Perfil Perfil { get; set; }
        public int PerfilId { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();



        [Display(Name = "Usuário")]
        [ForeignKey("IdentityUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        public Login()
        {
        }

        public Login(int id, string usuario, Perfil perfil)
        {
            Id = id;
            Usuario = usuario;           
            Perfil = perfil;
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
