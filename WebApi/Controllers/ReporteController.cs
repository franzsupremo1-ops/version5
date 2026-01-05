using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporte _reporteRepo;
        private readonly IMapper _mapper;

        public ReporteController(IReporte reporteRepo, IMapper mapper)
        {
            _reporteRepo = reporteRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var aguas = await _reporteRepo.All();
            if (!aguas.Any())
                return NotFound("no hay registros de agua que listar");
            var aguasDto = _mapper.Map<IEnumerable<AguaDTOs>>(aguas);
            return Ok(aguasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var agua = await _reporteRepo.ObtenerId(id);
            if (agua == null)
                return NotFound("registro de agua no encontrado");
            var aguaDto = _mapper.Map<AguaDTOs>(agua);
            return Ok(aguaDto);
        }
    }
}
