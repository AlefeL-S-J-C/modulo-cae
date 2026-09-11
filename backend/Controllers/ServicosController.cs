using GestaoConteudoCae.Api.Data;
using GestaoConteudoCae.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Controllers;

[ApiController, Route("api/conteudos/servicos")]
public class ServicosController(AppDbContext db) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll()=>Ok(await db.Servicos.Include(x=>x.Destaques).Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync());
    [HttpGet("admin")] public async Task<IActionResult> GetAdmin()=>Ok(await db.Servicos.Include(x=>x.Destaques).OrderBy(x=>x.Ordem).ToListAsync());
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id)=>await db.Servicos.Include(x=>x.Destaques).FirstOrDefaultAsync(x=>x.Id==id) is { } x?Ok(x):NotFound();

    [HttpPost]
    public async Task<IActionResult> Create(Servico m)
    {
        m.Id=0; db.Servicos.Add(m); await db.SaveChangesAsync(); return Ok(m);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Servico m)
    {
        var x=await db.Servicos.Include(x=>x.Destaques).FirstOrDefaultAsync(x=>x.Id==id);
        if(x is null)return NotFound();
        x.Nome=m.Nome;x.Categoria=m.Categoria;x.TituloCurto=m.TituloCurto;x.TextoCurto=m.TextoCurto;x.DescricaoCompleta=m.DescricaoCompleta;
        x.Icone=m.Icone;x.Destaque=m.Destaque;x.Ordem=m.Ordem;x.Ativo=m.Ativo;x.Detalhes=m.Detalhes;x.Contato=m.Contato;x.LinkExterno=m.LinkExterno;x.AtualizadoEm=DateTime.UtcNow;
        db.ServicoDestaques.RemoveRange(x.Destaques);
        x.Destaques=m.Destaques.Select(d=>new ServicoDestaque{Texto=d.Texto,Ordem=d.Ordem}).ToList();
        await db.SaveChangesAsync(); return Ok(x);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id){var x=await db.Servicos.FindAsync(id);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync();return NoContent();}
}
