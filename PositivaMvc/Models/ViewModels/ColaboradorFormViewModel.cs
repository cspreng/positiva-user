using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Models.ViewModels
{
    public class ColaboradorFormViewModel
    {
        public Colaborador Colaborador { get; set; }
        public ICollection<Empresa> Empresas { get; set; }
        public ICollection<Login> Logins { get; set; }
    }
}



