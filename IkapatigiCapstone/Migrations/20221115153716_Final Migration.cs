using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkapatigiCapstone.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
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
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    StatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles1",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

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
                    table.ForeignKey(
                        name: "FK_AddRequestDiagnostics_Users",
                        column: x => x.RequestedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Forum",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forum", x => x.ForumID);
                    table.ForeignKey(
                        name: "FK_Forum_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "HowTos",
                columns: table => new
                {
                    HowTosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: true),
                    DislikeCount = table.Column<int>(type: "int", nullable: true),
                    IsPublic = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    PictureCollectionFromID = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime", nullable: true),
                    ArticleBody = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowTos", x => x.HowTosID);
                    table.ForeignKey(
                        name: "FK_HowTos_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_HowTos_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
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
                    table.ForeignKey(
                        name: "FK_Tags_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    ForumID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Post_Forum",
                        column: x => x.ForumID,
                        principalTable: "Forum",
                        principalColumn: "ForumID");
                    table.ForeignKey(
                        name: "FK_Post_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PlantDiseases",
                columns: table => new
                {
                    PlantDiseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ImageOfDisease = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_PlantDiseases_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PostReply",
                columns: table => new
                {
                    PostReplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReply", x => x.PostReplyID);
                    table.ForeignKey(
                        name: "FK_PostReply_Post",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "PostID");
                    table.ForeignKey(
                        name: "FK_PostReply_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Diagnostics",
                columns: table => new
                {
                    DiagnosticsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureCollectionFromID = table.Column<int>(type: "int", nullable: true),
                    CureID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false),
                    PlantDiseaseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostics_1", x => x.DiagnosticsID);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Cures",
                        column: x => x.CureID,
                        principalTable: "Cures",
                        principalColumn: "CureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnostics_PlantDiseases",
                        column: x => x.PlantDiseaseID,
                        principalTable: "PlantDiseases",
                        principalColumn: "PlantDiseaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_Diagnostics_Tags",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddRequestDiagnostics_RequestedUserID",
                table: "AddRequestDiagnostics",
                column: "RequestedUserID");

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
                name: "IX_Forum_UserID",
                table: "Forum",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HowTos_StatusID",
                table: "HowTos",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_HowTos_UserID",
                table: "HowTos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDiseases_TagID",
                table: "PlantDiseases",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDiseases_UserID",
                table: "PlantDiseases",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_ForumID",
                table: "Post",
                column: "ForumID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserID",
                table: "Post",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostReply_PostID",
                table: "PostReply",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostReply_UserID",
                table: "PostReply",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserID",
                table: "Tags",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddRequestDiagnostics");

            migrationBuilder.DropTable(
                name: "Diagnostics");

            migrationBuilder.DropTable(
                name: "HowTos");

            migrationBuilder.DropTable(
                name: "PostReply");

            migrationBuilder.DropTable(
                name: "Cures");

            migrationBuilder.DropTable(
                name: "PlantDiseases");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Forum");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
