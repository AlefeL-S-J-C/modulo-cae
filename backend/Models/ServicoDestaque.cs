using System.Text.Json.Serialization;

namespace GestaoConteudoCae.Api.Models;
public class ServicoDestaque {
    public int Id { get; set; }
    public int ServicoId { get; set; }
    public string Texto { get; set; } = "";
    public int Ordem { get; set; }
    [JsonIgnore]
    public Servico? Servico { get; set; }
}
