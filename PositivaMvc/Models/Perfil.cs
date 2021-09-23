using System.Collections.Generic;

namespace PositivaMvc.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string TipoPerfil { get; set; }
        public ICollection<Login> Logins { get; set; } = new List<Login>();

        public Perfil()
        {
        }

        public Perfil(int id, string tipoPerfil)
        {
            Id = id;
            TipoPerfil = tipoPerfil;
        }

        public void AddLogin(Login login)
        {
            Logins.Add(login);
        }

        public void RemoveColaborador(Login login)
        {
            Logins.Remove(login);
        }
    }


}
