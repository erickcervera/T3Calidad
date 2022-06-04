using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CERNA_T3.Models;

namespace CERNA_T3.Repository
{
    public interface IHomeRepository
    {
        List<Historia> GetHistorias();
        List<Raza> GetRazas(int IdEspecie);
        List<Sexo> GetSexos();
        List<Especie> GetEspecies();
        void SaveHistory(Historia historia);
        Usuario GetUsuario(string username, string password);
        void SaveUsuario(Usuario user);
        List<Usuario> GetUsuarios();
    }
    public class HomeRepository : IHomeRepository
    {
        private readonly Configuracion context;

        public HomeRepository(Configuracion context)
        {
            this.context = context;
        }

        public List<Especie> GetEspecies()
        {
            return context.Especies.ToList();
        }

        public List<Historia> GetHistorias()
        {
            return context.Historias
                .Include(o => o.Sexo)
                .Include(o => o.Especie)
                .Include(o => o.Raza)
                .ToList();
        }

        public List<Raza> GetRazas(int IdEspecie)
        {
            return context.Razas
                .Where(o => o.EspecieId == IdEspecie)
                .ToList();
        }

        public List<Sexo> GetSexos()
        {
            return context.Sexos.ToList();
        }

        public void SaveHistory(Historia historia)
        {
            context.Add(historia);
            context.SaveChanges();
        }
        public Usuario GetUsuario(string username, string password)
        {
            return context.Usuarios.Where(o => o.Username == username && o.Password == password).FirstOrDefault();
        }

        public List<Usuario> GetUsuarios()
        {
            return context.Usuarios.ToList();
        }

        public void SaveUsuario(Usuario user)
        {
            context.Usuarios.Add(user);
            context.SaveChanges();
        }
    }
}
