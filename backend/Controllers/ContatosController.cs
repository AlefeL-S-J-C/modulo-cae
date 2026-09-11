using GestaoConteudoCae.Api.Data;
using GestaoConteudoCae.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Controllers;

[ApiController, Route("api/cae/contatos")]
public class ContatosController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await db.Contatos.OrderBy(x=>x.Ordem).ToListAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => await db.Contatos.FindAsync(id) is { } x ? Ok(x) : NotFound();
    [HttpPost] public async Task<IActionResult> Create(ContatoCae model) { db.Contatos.Add(model); await db.SaveChangesAsync(); return CreatedAtAction(nameof(Get), new { id=model.Id }, model); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, ContatoCae model) { var x=await db.Contatos.FindAsync(id); if(x is null)return NotFound(); x.Tipo=model.Tipo;x.Numero=model.Numero;x.Descricao=model.Descricao;x.Ativo=model.Ativo;x.Ordem=model.Ordem;x.AtualizadoEm=DateTime.UtcNow;await db.SaveChangesAsync();return Ok(x); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) { var x=await db.Contatos.FindAsync(id);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync();return NoContent(); }
}
