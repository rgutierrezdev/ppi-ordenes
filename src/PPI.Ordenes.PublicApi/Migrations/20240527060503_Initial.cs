using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPI.Ordenes.PublicApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCuenta = table.Column<int>(type: "int", nullable: false),
                    PrimerNombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    SegundoNombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TercerNombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CuentaComitente = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCuenta = table.Column<int>(type: "int", nullable: false),
                    NombreActivo = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Operacion = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
