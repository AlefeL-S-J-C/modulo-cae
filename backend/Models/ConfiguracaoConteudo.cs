namespace GestaoConteudoCae.Api.Models;
public class ConfiguracaoConteudo {
    public int Id { get; set; }
    public string Chave { get; set; } = "";
    public string Valor { get; set; } = "";
    public bool Ativo { get; set; } = true;
}
