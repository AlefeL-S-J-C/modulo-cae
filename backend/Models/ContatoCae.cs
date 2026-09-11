namespace GestaoConteudoCae.Api.Models;
public class ContatoCae {
    public int Id { get; set; }
    public string Tipo { get; set; } = "Telefone";
    public string Numero { get; set; } = "";
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public int Ordem { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
