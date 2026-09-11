namespace GestaoConteudoCae.Api.Models;
public class HorarioAtendimento {
    public int Id { get; set; }
    public string DiaSemana { get; set; } = "";
    public string HoraInicio { get; set; } = "";
    public string HoraFim { get; set; } = "";
    public bool Fechado { get; set; }
    public bool Ativo { get; set; } = true;
    public int Ordem { get; set; }
}
