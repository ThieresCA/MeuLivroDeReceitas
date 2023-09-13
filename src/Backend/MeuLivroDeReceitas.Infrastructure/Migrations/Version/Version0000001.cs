using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Version
{
    [Migration(1, "Cria a tabela usuário")]
    public class Version0000001 : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            //criando a tabela e passando como parâmetro para a função estática que já cria os 2 campos Id e CreateDate
            var table = BaseVersion.BaseColumns(Create.Table("Usuario"));

            table.WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Phone").AsString(14).NotNullable();
        }
    }
}
