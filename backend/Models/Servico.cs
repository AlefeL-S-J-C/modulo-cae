namespace GestaoConteudoCae.Api.Models;
public class Servico {
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Categoria { get; set; } = "";
    public string TituloCurto { get; set; } = "";
    public string TextoCurto { get; set; } = "";
    public string DescricaoCompleta { get; set; } = "";
    public string? Icone { get; set; }
    public bool Destaque { get; set; }
    public int Ordem { get; set; }
    public bool Ativo { get; set; } = true;
    public string? Detalhes { get; set; }
    public string? Contato { get; set; }
    public string? LinkExterno { get; set; }
    public ICollection<ServicoDestaque> Destaques { get; set; } = [];
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
