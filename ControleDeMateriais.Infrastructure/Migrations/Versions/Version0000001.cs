using FluentMigrator;

namespace ControleDeMateriais.Infrastructure.Migrations.Versions;

[Migration((long)VersionNumber.CreateUserTable, "Create user table")]
public class Version0000001 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = VersionBase.InsertStandardColumns(Create.Table("User"));

        table
             .WithColumn("Name").AsString(100).NotNullable()
             .WithColumn("Email").AsString(100).NotNullable()
             .WithColumn("Password").AsString(2000).NotNullable()
             .WithColumn("Telephone").AsString(14).NotNullable();
    }
}
