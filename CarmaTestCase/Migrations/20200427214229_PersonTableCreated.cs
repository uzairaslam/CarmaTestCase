using Microsoft.EntityFrameworkCore.Migrations;

namespace CarmaTestCase.Migrations
{
    public partial class PersonTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_document = table.Column<string>(maxLength: 100, nullable: true),
                    phone = table.Column<string>(maxLength: 50, nullable: true),
                    alternative_id = table.Column<string>(maxLength: 50, nullable: true),
                    driving_license = table.Column<string>(maxLength: 50, nullable: true),
                    first_name = table.Column<string>(maxLength: 50, nullable: true),
                    last_name = table.Column<string>(maxLength: 50, nullable: true),
                    sex = table.Column<string>(maxLength: 20, nullable: true),
                    education = table.Column<string>(maxLength: 50, nullable: true),
                    marital_status = table.Column<string>(maxLength: 50, nullable: true),
                    children = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
