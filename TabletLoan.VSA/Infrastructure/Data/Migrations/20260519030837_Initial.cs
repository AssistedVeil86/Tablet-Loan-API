using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TabletLoan.VSA.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LoanSys");

            migrationBuilder.CreateTable(
                name: "Tablets",
                schema: "LoanSys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ServoPin = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    SwitchPin = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    AirDroidDeviceId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AirDroidCDeviceId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tablets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                schema: "LoanSys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentCif = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    StudentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StudentLastname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LoanStartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LoanEndsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TabletId = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Tablets_TabletId",
                        column: x => x.TabletId,
                        principalSchema: "LoanSys",
                        principalTable: "Tablets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_TabletId",
                schema: "LoanSys",
                table: "Loans",
                column: "TabletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans",
                schema: "LoanSys");

            migrationBuilder.DropTable(
                name: "Tablets",
                schema: "LoanSys");
        }
    }
}
