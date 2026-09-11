using GestaoConteudoCae.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoConteudoCae.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (!await db.Contatos.AnyAsync())
        {
            db.Contatos.AddRange(
                new ContatoCae { Tipo = "Telefone", Numero = "(65) 3688-6101", Descricao = "CAE", Ativo = true, Ordem = 1 },
                new ContatoCae { Tipo = "Telefone", Numero = "(65) 3688-6080", Descricao = "CAE", Ativo = true, Ordem = 2 },
                new ContatoCae { Tipo = "WhatsApp", Numero = "(65) 9648-4328", Descricao = "Atendimento", Ativo = true, Ordem = 3 }
            );
        }

        if (!await db.Horarios.AnyAsync())
        {
            db.Horarios.AddRange(
                new HorarioAtendimento { DiaSemana = "Segunda a sexta-feira", HoraInicio = "08:00", HoraFim = "22:00", Fechado = false, Ativo = true, Ordem = 1 },
                new HorarioAtendimento { DiaSemana = "Sábado", HoraInicio = "08:00", HoraFim = "12:00", Fechado = false, Ativo = true, Ordem = 2 },
                new HorarioAtendimento { DiaSemana = "Domingo e feriados", HoraInicio = "", HoraFim = "", Fechado = true, Ativo = true, Ordem = 3 }
            );
        }

        if (!await db.Faqs.AnyAsync())
        {
            db.Faqs.AddRange(
                new Faq { Titulo = "Como consultar minhas notas?", Resposta = "Acesse a seção \"Notas e Faltas\" no Portal do Aluno.", Icone = "pi pi-star", CorIcone = "#8b5cf6", Categoria = "Acadêmico", Ordem = 1, Ativo = true },
                new Faq { Titulo = "Como consultar minhas faltas?", Resposta = "Vá para \"Notas e Faltas\" para consultar sua frequência.", Icone = "pi pi-times-circle", CorIcone = "#ef4444", Categoria = "Acadêmico", Ordem = 2, Ativo = true },
                new Faq { Titulo = "Como visualizar meu horário acadêmico?", Resposta = "Clique em \"Horário\" no menu principal.", Icone = "pi pi-calendar", CorIcone = "#06b6d4", Categoria = "Acadêmico", Ordem = 3, Ativo = true },
                new Faq { Titulo = "Como acessar o Portal AVA?", Resposta = "Use o acesso \"Portal AVA\" no Portal do Aluno.", Icone = "pi pi-book", CorIcone = "#3b82f6", Categoria = "Portal", Ordem = 4, Ativo = true },
                new Faq { Titulo = "Como consultar minhas mensalidades?", Resposta = "Acesse \"Financeiro\" para visualizar mensalidades e pagamentos.", Icone = "pi pi-wallet", CorIcone = "#10b981", Categoria = "Financeiro", Ordem = 5, Ativo = true },
                new Faq { Titulo = "Como consultar minhas pendências?", Resposta = "Acesse \"Pendências\" para visualizar itens que precisam de atenção.", Icone = "pi pi-exclamation-triangle", CorIcone = "#f59e0b", Categoria = "Portal", Ordem = 6, Ativo = true }
            );
        }

        if (!await db.Servicos.AnyAsync())
        {
            var servicos = new[]
            {
                new Servico { Nome="Clínica Integrada", Categoria="Saúde", TituloCurto="Clínica Integrada", TextoCurto="Atendimento humanizado e prática acadêmica em diversas áreas da saúde.", DescricaoCompleta="A Clínica Integrada do UNIVAG une formação prática e atendimento à comunidade, com uma estrutura multidisciplinar.", Icone="pi pi-heart", Contato="(65) 3688-6130 / 6133", LinkExterno="https://www.univag.com.br/item/7/clinica-integrada/", Destaque=true, Ordem=1, Ativo=true },
                new Servico { Nome="Farmácia Escola", Categoria="Saúde", TituloCurto="Farmácia Escola", TextoCurto="Assistência farmacêutica especializada e medicamentos personalizados.", DescricaoCompleta="A Farmácia UNIVAG oferece assistência farmacêutica à população.", Icone="pi pi-plus-circle", Contato="(65) 3685-7200", LinkExterno="https://www.univag.com.br/item/1/farmacia-escola/", Ordem=2, Ativo=true },
                new Servico { Nome="Academia Coloiado Fitness", Categoria="Bem-estar", TituloCurto="Academia Coloiado Fitness", TextoCurto="Ambiente climatizado e familiar para cuidar da saúde e da qualidade de vida.", DescricaoCompleta="A academia do UNIVAG oferece musculação e profissionais qualificados.", Icone="pi pi-bolt", Contato="(65) 99256-3759", LinkExterno="https://www.univag.com.br/item/2/academia/", Ordem=3, Ativo=true },
                new Servico { Nome="UNIVAG Profissões", Categoria="Carreira", TituloCurto="UNIVAG Profissões", TextoCurto="Experiências práticas para conhecer possibilidades profissionais.", DescricaoCompleta="O UNIVAG aproxima estudantes do ensino médio da realidade universitária.", Icone="pi pi-briefcase", Contato="Marketing UNIVAG", LinkExterno="https://www.univag.com.br/", Ordem=4, Ativo=true },
                new Servico { Nome="UNIVAG Social", Categoria="Comunidade", TituloCurto="UNIVAG Social", TextoCurto="Projetos e ações que transformam conhecimento em impacto positivo.", DescricaoCompleta="Programa de responsabilidade social que conecta alunos, professores e colaboradores.", Icone="pi pi-users", Contato="social@univag.edu.br", LinkExterno="https://www.univag.com.br/sobre-o-univag/30/univag-social/", Ordem=5, Ativo=true },
                new Servico { Nome="UNIVAG Idiomas", Categoria="Formação", TituloCurto="UNIVAG Idiomas", TextoCurto="Cursos para ampliar comunicação, formação e oportunidades.", DescricaoCompleta="Cursos presenciais de idiomas e língua portuguesa para diferentes públicos.", Icone="pi pi-globe", Contato="(65) 3388-5700 / 5710", LinkExterno="https://www.univag.com.br/cursos/6/idiomas/", Ordem=6, Ativo=true },
                new Servico { Nome="Núcleo de Práticas Jurídicas", Categoria="Direito", TituloCurto="Núcleo de Práticas Jurídicas", TextoCurto="Formação prática em Direito e atendimento jurídico à comunidade.", DescricaoCompleta="Estudantes de Direito vivenciam a rotina profissional sob supervisão.", Icone="pi pi-building", Contato="(65) 3688-6021 / 6022", LinkExterno="https://www.univag.com.br/item/8/nucleo-de-praticas-juridicas-npj/", Ordem=7, Ativo=true },
                new Servico { Nome="CAE — Central de Atendimento", Categoria="Atendimento", TituloCurto="CAE — Central de Atendimento", TextoCurto="Ponto de apoio para serviços acadêmicos, administrativos e financeiros.", DescricaoCompleta="A CAE centraliza atendimentos para estudantes e público em geral.", Icone="pi pi-headphones", Contato="(65) 3688-6101 • (65) 3688-6080 • (65) 9648-4328", LinkExterno="https://www.univag.com.br/", Destaque=true, Ordem=8, Ativo=true }
            };

            db.Servicos.AddRange(servicos);
            await db.SaveChangesAsync();

            foreach (var s in servicos)
            {
                db.ServicoDestaques.AddRange(
                    new ServicoDestaque { ServicoId = s.Id, Texto = "Informação configurável pelo CAE", Ordem = 1 },
                    new ServicoDestaque { ServicoId = s.Id, Texto = "Exibido dinamicamente no Portal do Aluno", Ordem = 2 }
                );
            }
        }

        await db.SaveChangesAsync();
    }
}
