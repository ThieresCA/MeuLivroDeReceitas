using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Infrastructure.Data.Version
{
    public class BaseVersion
    {
        //função que recebe a tabela como parâmetro e devolve a tabela com as 2 colunas criadas
        public static ICreateTableColumnOptionOrWithColumnSyntax BaseColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
        {
            return table.WithColumn("Id").AsInt64().PrimaryKey().Identity()
                   .WithColumn("CreateDate").AsDateTime().NotNullable();
        }
    }
}
