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
    public class AguaController : ControllerBase
    {
        private readonly IAgua _aguaRepo;
        private readonly IMapper _mapper;
        private readonly CrearAgua _crearAgua;

        public AguaController(IAgua aguaRepo, IMapper mapper, CrearAgua crearAgua)
        {
            _aguaRepo = aguaRepo;
            _mapper = mapper;
            _crearAgua = crearAgua;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var aguas = await _aguaRepo.All();
            if (!aguas.Any())
                return NotFound("no hay registros de agua que listar");
            var aguasDto = _mapper.Map<IEnumerable<AguaDTOs>>(aguas);
            return Ok(aguasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var agua = await _aguaRepo.ObtenerId(id);
            if (agua == null)
                return NotFound("registro de agua no encontrado");
            var aguaDto = _mapper.Map<AguaDTOs>(agua);
            return Ok(aguaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AguaDTOs aguaDTO)
        {
            try
            {
                var agua = _mapper.Map<Agua>(aguaDTO);
                await _crearAgua.EjecutarAsync(agua);
                return CreatedAtAction(nameof(GetById), new { id = agua.Id }, aguaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AguaDTOs aguaDTO)
        {
            try
            {
                var aguaExistente = await _aguaRepo.ObtenerId(id);
                if (aguaExistente == null)
                    return NotFound("registro de agua no encontrado");

                _mapper.Map(aguaDTO, aguaExistente);
                await _aguaRepo.Actualizar(aguaExistente);
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
            var agua = await _aguaRepo.ObtenerId(id);
            if (agua == null)
                return NotFound("registro de agua no encontrado");

            await _aguaRepo.Eliminar(id);
            return NoContent();
        }
    }
}
