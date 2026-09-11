namespace GestaoConteudoCae.Api.Models;
public class Faq {
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Resposta { get; set; } = "";
    public string? Categoria { get; set; }
    public string? Icone { get; set; }
    public string? CorIcone { get; set; }
    public int Ordem { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
