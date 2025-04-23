using Microsoft.EntityFrameworkCore;
using ElChenVuelveDashb.Servicios.Contrato;

namespace ElChenVuelveDashb.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ToyoNoToyContext _dbContext;

        public UsuarioService(ToyoNoToyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string username, string password)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
