using GestaoConteudoCae.Api.Data;
using GestaoConteudoCae.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Controllers;

[ApiController, Route("api/cae/horarios")]
public class HorariosController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await db.Horarios.OrderBy(x => x.Ordem).ToListAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => await db.Horarios.FindAsync(id) is { } x ? Ok(x) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(HorarioAtendimento m) { db.Horarios.Add(m); await db.SaveChangesAsync(); return Ok(m); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, HorarioAtendimento m) { var x = await db.Horarios.FindAsync(id); if (x is null) return NotFound(); x.DiaSemana = m.DiaSemana; x.HoraInicio = m.HoraInicio; x.HoraFim = m.HoraFim; x.Fechado = m.Fechado; x.Ativo = m.Ativo; x.Ordem = m.Ordem; await db.SaveChangesAsync(); return Ok(x); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) { var x = await db.Horarios.FindAsync(id); if (x is null) return NotFound(); db.Remove(x); await db.SaveChangesAsync(); return NoContent(); }
}
