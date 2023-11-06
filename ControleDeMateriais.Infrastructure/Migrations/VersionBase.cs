using FluentMigrator.Builders.Create.Table;

namespace ControleDeMateriais.Infrastructure.Migrations;
public static class VersionBase
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertStandardColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreationDate").AsDateTime().NotNullable();
    }
}
