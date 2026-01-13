using Application.Builders;
using Application.DTOs;
using Application.UseCases;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepo;
        private readonly IMapper _mapper;
        private readonly CrearUsuario _crearUsuario;

        public UsuarioController(IUsuario usuarioRepo, IMapper mapper, CrearUsuario crearUsuario)
        {
            _usuarioRepo = usuarioRepo;
            _mapper = mapper;
            _crearUsuario = crearUsuario;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var usuarios = await _usuarioRepo.All();
            if (!usuarios.Any())
                return NotFound("no hay usuarios que listar");
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTOs>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var usuario = await _usuarioRepo.ObtenerId(id);
            if (usuario == null)
                return NotFound("usuario no encontrado");
            var usuarioDto = _mapper.Map<UsuarioDTOs>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTOs usuarioDTO)
        {
            try
            {
                var usuario = new UsuarioBuilder()
                    .ConNombre(usuarioDTO.Nombre)
                    .ConApellido(usuarioDTO.Apellido)
                    .ConRol(usuarioDTO.Rol ?? "Usuario")
                    .Build();

                usuario.Id = Guid.NewGuid();

                await _crearUsuario.EjecutarAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuarioDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UsuarioDTOs usuarioDTO)
        {
            try
            {
                var usuarioExistente = await _usuarioRepo.ObtenerId(id);
                if (usuarioExistente == null)
                    return NotFound("usuario no encontrado");

                _mapper.Map(usuarioDTO, usuarioExistente);
                await _usuarioRepo.Actualizar(usuarioExistente);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var usuario = await _usuarioRepo.ObtenerId(id);
            if (usuario == null)
                return NotFound("usuario no encontrado");

            await _usuarioRepo.Eliminar(id);
            return NoContent();
        }
    }
}
