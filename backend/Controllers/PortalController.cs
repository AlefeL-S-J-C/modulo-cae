using GestaoConteudoCae.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Controllers;

[ApiController, Route("api/portal")]
public class PortalController(AppDbContext db) : ControllerBase
{
    [HttpGet("cae")]
    public async Task<IActionResult> GetCae()
    {
        var contatos = await db.Contatos.Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync();
        var horarios = await db.Horarios.Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync();
        var faqs = await db.Faqs.Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync();
        return Ok(new { contatos, horarios, faqs });
    }

    [HttpGet("servicos")]
    public async Task<IActionResult> GetServicos() =>
        Ok(await db.Servicos.Include(x=>x.Destaques).Where(x=>x.Ativo).OrderBy(x=>x.Ordem).ToListAsync());
}
