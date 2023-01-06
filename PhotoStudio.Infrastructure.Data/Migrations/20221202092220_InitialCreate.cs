using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "Timestamp", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "Timestamp", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBook",
                columns: table => new
                {
                    PhotoBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    PortadaPrice = table.Column<decimal>(type: "money", nullable: false),
                    PriceByPage = table.Column<decimal>(type: "money", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "Timestamp", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBook", x => x.PhotoBookId);
                    table.ForeignKey(
                        name: "FK_PhotoBooks_Material",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoBooks_Sizes",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material",
                table: "Material",
                column: "MaterialName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBook_MaterialId",
                table: "PhotoBook",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBook_MaterialId_SizeId_Unique",
                table: "PhotoBook",
                columns: new[] { "SizeId", "MaterialId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Size_Unique",
                table: "Size",
                column: "Size",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoBook");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Size");
        }
    }
}
