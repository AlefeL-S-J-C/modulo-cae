using GestaoConteudoCae.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ContatoCae> Contatos => Set<ContatoCae>();
    public DbSet<HorarioAtendimento> Horarios => Set<HorarioAtendimento>();
    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<Servico> Servicos => Set<Servico>();
    public DbSet<ServicoDestaque> ServicoDestaques => Set<ServicoDestaque>();
    public DbSet<ConfiguracaoConteudo> Configuracoes => Set<ConfiguracaoConteudo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContatoCae>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Tipo).HasMaxLength(30).IsRequired();
            e.Property(x => x.Numero).HasMaxLength(40).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(120);
        });

        modelBuilder.Entity<HorarioAtendimento>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.DiaSemana).HasMaxLength(30).IsRequired();
            e.Property(x => x.HoraInicio).HasMaxLength(5).IsRequired();
            e.Property(x => x.HoraFim).HasMaxLength(5).IsRequired();
        });

        modelBuilder.Entity<Faq>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).HasMaxLength(180).IsRequired();
            e.Property(x => x.Resposta).IsRequired();
            e.Property(x => x.Icone).HasMaxLength(80);
            e.Property(x => x.CorIcone).HasMaxLength(20);
            e.Property(x => x.Categoria).HasMaxLength(80);
        });

        modelBuilder.Entity<Servico>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).HasMaxLength(160).IsRequired();
            e.Property(x => x.Categoria).HasMaxLength(80).IsRequired();
            e.Property(x => x.TituloCurto).HasMaxLength(180).IsRequired();
            e.Property(x => x.TextoCurto).HasMaxLength(500).IsRequired();
            e.Property(x => x.DescricaoCompleta).IsRequired();
            e.Property(x => x.Icone).HasMaxLength(100);
            e.Property(x => x.Contato).HasMaxLength(250);
            e.Property(x => x.LinkExterno).HasMaxLength(500);
            e.HasMany(x => x.Destaques).WithOne(x => x.Servico).HasForeignKey(x => x.ServicoId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ServicoDestaque>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Texto).HasMaxLength(250).IsRequired();
        });

        modelBuilder.Entity<ConfiguracaoConteudo>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Chave).HasMaxLength(100).IsRequired();
            e.Property(x => x.Valor).IsRequired();
            e.HasIndex(x => x.Chave).IsUnique();
        });
    }
}
