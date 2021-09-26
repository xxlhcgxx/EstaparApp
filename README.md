# Estapar

@{
    ViewData["Title"] = "Home Page";
}

Programa com as seguintes funcionalidades:

- Marcas de veíclos: CRUD
- Modelos de veículos: CRUD
- Manobristas: CRUD
- Registro: CRUD

Configuração da base de dados - Migration

- Atualizar string de conexão nos arquivos: appsettings.json (ConnectionStrings/DefaultConnection) e Data/ApplicationDbContext (optionsBuilder.UseSqlServer)
- Executar no console o comando: Update-Database

Configuração da base de dados - Manual

- Criação da base de dados: EstaparDataBase
- Executar scripts da pasta EstaparApp/Database/ScriptsTable na ordem de execução
- Executar scripts da pasta EstaparApp/Database/ScriptsInsert na ordem de execução
- Atualizar string de conexão nos arquivos: appsettings.json (ConnectionStrings/DefaultConnection) e Data/ApplicationDbContext (optionsBuilder.UseSqlServer)

Configuração da base de dados - Restaurar Banco de Dados

- Restaurar backup do banco de dados da pasta: EstaparApp/Database/BkpBanco/Bkp_EstaparDataBase.bak
- Atualizar string de conexão nos arquivos: appsettings.json (ConnectionStrings/DefaultConnection) e Data/ApplicationDbContext (optionsBuilder.UseSqlServer)
