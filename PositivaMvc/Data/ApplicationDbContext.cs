using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PositivaMvc.Models.ViewModels;


namespace PositivaMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PositivaMvc.Models.Empresa> Empresa { get; set; }
        public DbSet<PositivaMvc.Models.Colaborador> Colaborador { get; set; }
        public DbSet<PositivaMvc.Models.Avaliacao> Avaliacao { get; set; }
        public DbSet<PositivaMvc.Models.GrupoPerguntas> GrupoPerguntas { get; set; }
        public DbSet<PositivaMvc.Models.Nota> Nota { get; set; }
        public DbSet<PositivaMvc.Models.Pergunta> Pergunta { get; set; }
        public DbSet<PositivaMvc.Models.Login> Login { get; set; }

        public DbSet<PositivaMvc.Models.Perfil> Perfil { get; set; }
        public DbSet<IdentityUser> Usuario { get; set; }
    }
}
