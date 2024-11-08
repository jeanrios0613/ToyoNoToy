using Microsoft.EntityFrameworkCore;
using ElChenVuelveDashb.Models;


namespace ElChenVuelveDashb.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string username,string password);
        Task<Usuario> SaveUsuario(Usuario Modelo);
    }
}
