using GestaoConteudoCae.Api.Data;
using GestaoConteudoCae.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Controllers;

[ApiController, Route("api/cae/faqs")]
public class FaqsController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(await db.Faqs.Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync());
    [HttpGet("admin")] public async Task<IActionResult> GetAdmin()=>Ok(await db.Faqs.OrderBy(x=>x.Ordem).ToListAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id)=>await db.Faqs.FindAsync(id) is { } x?Ok(x):NotFound();
    [HttpPost] public async Task<IActionResult> Create(Faq m){db.Faqs.Add(m);await db.SaveChangesAsync();return Ok(m);}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,Faq m){var x=await db.Faqs.FindAsync(id);if(x is null)return NotFound();x.Titulo=m.Titulo;x.Resposta=m.Resposta;x.Categoria=m.Categoria;x.Icone=m.Icone;x.CorIcone=m.CorIcone;x.Ordem=m.Ordem;x.Ativo=m.Ativo;x.AtualizadoEm=DateTime.UtcNow;await db.SaveChangesAsync();return Ok(x);}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await db.Faqs.FindAsync(id);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync();return NoContent();}
}
