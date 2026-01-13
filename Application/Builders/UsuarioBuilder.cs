using Domain.Entities;

namespace Application.Builders
{
    public class UsuarioBuilder
    {
        private readonly Usuario _usuario = new();

        public UsuarioBuilder ConNombre(string nombre)
        {
            _usuario.Nombre = nombre;
            return this;
        }

        public UsuarioBuilder ConApellido(string? apellido)  
        {
            _usuario.Apellido = apellido;
            return this;
        }

        public UsuarioBuilder ConRol(string rol)
        {
            _usuario.Rol = rol;
            return this;
        }

        public Usuario Build()
        {
            _usuario.Id = Guid.NewGuid(); 
            return _usuario;
        }
    }
}
