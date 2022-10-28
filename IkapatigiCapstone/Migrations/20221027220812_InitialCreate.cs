using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkapatigiCapstone.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddRequestDiagnostics",
                columns: table => new
                {
                    AddRequestDiagnosticID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TagName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CureName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DiseaseName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SRP = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ApprovedUserID = table.Column<int>(type: "int", nullable: true),
                    RequestedUserID = table.Column<int>(type: "int", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddRequestDiagnostics", x => x.AddRequestDiagnosticID);
                });

            migrationBuilder.CreateTable(
                name: "Cures",
                columns: table => new
                {
                    CureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CureName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    SRP = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cures", x => x.CureID);
                });

            migrationBuilder.CreateTable(
                name: "PlantDiseases-no cures",
                columns: table => new
                {
                    PlantDiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ImageOfDisease = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TagID = table.Column<int>(type: "int", nullable: true),
                    CureID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDiseases", x => x.PlantDiseaseID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Password = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DisplayName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AccountNumber = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime", nullable: true),
                    RemainingSubscriptionDays = table.Column<int>(type: "int", nullable: true),
                    CanceledSubscription = table.Column<bool>(type: "bit", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    SubscriptionID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantDiseases",
                columns: table => new
                {
                    PlantDiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ImageOfDisease = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: true),
                    CureID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantDiseases", x => x.PlantDiseaseID);
                    table.ForeignKey(
                        name: "FK_PlantDiseases_Tags",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "Diagnostics",
                columns: table => new
                {
                    DiagnosticsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureCollectionFromID = table.Column<int>(type: "int", nullable: true),
                    CureID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: true),
                    PlantDiseaseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostics_1", x => x.DiagnosticsID);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Cures",
                        column: x => x.CureID,
                        principalTable: "Cures",
                        principalColumn: "CureID");
                    table.ForeignKey(
                        name: "FK_Diagnostics_PlantDiseases",
                        column: x => x.PlantDiseaseID,
                        principalTable: "PlantDiseases",
                        principalColumn: "PlantDiseaseID");
                    table.ForeignKey(
                        name: "FK_Diagnostics_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Diagnostics_Tags",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_CureID",
                table: "Diagnostics",
                column: "CureID");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_PlantDiseaseID",
                table: "Diagnostics",
                column: "PlantDiseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_StatusID",
                table: "Diagnostics",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_TagID",
                table: "Diagnostics",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDiseases_TagID",
                table: "PlantDiseases",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddRequestDiagnostics");

            migrationBuilder.DropTable(
                name: "Diagnostics");

            migrationBuilder.DropTable(
                name: "PlantDiseases-no cures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cures");

            migrationBuilder.DropTable(
                name: "PlantDiseases");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
