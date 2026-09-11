# Gestão de Conteúdo do CAE

Módulo administrativo para controlar os conteúdos consumidos pelo Portal do Aluno.

## Stack

- Frontend: Vue 3 + Vite + PrimeVue 4 + Tailwind CSS
- Backend: ASP.NET Core / .NET 10
- ORM: Entity Framework Core 10
- Banco: SQL Server
- API REST
- Swagger

## Conteúdo gerenciado

1. Central de Atendimento
   - telefones/canais
   - status
   - ordem
2. Horários
   - dia
   - início/fim
   - fechado
   - status
   - ordem
3. Central de Ajuda
   - pergunta
   - resposta
   - categoria
   - ícone
   - cor
   - ordem
   - publicação
4. Serviços
   - nome
   - categoria
   - título curto
   - texto curto
   - descrição completa
   - ícone
   - destaque
   - ordem
   - publicação
   - detalhes
   - contato
   - link externo
   - lista de destaques

## Como executar o backend

```bash
cd backend
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Swagger:
`https://localhost:7047/swagger`

## Como executar o frontend

```bash
cd frontend
npm install
npm run dev
```

Se a API estiver em outro endereço:

```env
VITE_API_URL=https://localhost:7047/api
```

## Integração com o Portal do Aluno

Os componentes de integração estão em `frontend/src/portal-integration`.

Endpoints públicos para o Portal:

- `GET /api/portal/cae`
- `GET /api/portal/servicos`

O princípio é: banco/API é a fonte de verdade; o Portal do Aluno não deve mais manter telefones, horários, FAQs ou serviços hardcoded.

## Próxima camada recomendada

Para produção, adicionar:
- autenticação/autorização por perfil;
- auditoria de alterações;
- versionamento/publicação;
- validação FluentValidation;
- paginação server-side;
- logs estruturados;
- tratamento global de exceções;
- armazenamento de imagens/ícones;
- testes unitários e de integração.
