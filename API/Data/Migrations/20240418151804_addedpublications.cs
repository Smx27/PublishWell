using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedpublications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationName = table.Column<string>(type: "TEXT", maxLength: 10000, nullable: false),
                    PublicationDescription = table.Column<string>(type: "TEXT", nullable: true),
                    PublicationImage = table.Column<string>(type: "TEXT", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublicationAuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicationContent = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AttachedDocumets = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_AspNetUsers_PublicationAuthorId",
                        column: x => x.PublicationAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationCategorie",
                columns: table => new
                {
                    PublicationCategorieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategorieId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationCategorie", x => x.PublicationCategorieId);
                    table.ForeignKey(
                        name: "FK_PublicationCategorie_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationCategorie_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationID = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsEdited = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationComment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationComment_Publications_PublicationID",
                        column: x => x.PublicationID,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationLike",
                columns: table => new
                {
                    PublicationLikeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationLike", x => x.PublicationLikeId);
                    table.ForeignKey(
                        name: "FK_PublicationLike_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationLike_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    PublicationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationTag_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastViewDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ViewerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationView_AspNetUsers_ViewerId",
                        column: x => x.ViewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationView_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationCategorie_CategorieId",
                table: "PublicationCategorie",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationCategorie_PublicationId",
                table: "PublicationCategorie",
                column: "PublicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicationComment_PublicationID",
                table: "PublicationComment",
                column: "PublicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationComment_UserID",
                table: "PublicationComment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationLike_AppUserId",
                table: "PublicationLike",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationLike_PublicationId",
                table: "PublicationLike",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublicationAuthorId",
                table: "Publications",
                column: "PublicationAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTag_PublicationId",
                table: "PublicationTag",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationView_PublicationId",
                table: "PublicationView",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationView_ViewerId",
                table: "PublicationView",
                column: "ViewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationCategorie");

            migrationBuilder.DropTable(
                name: "PublicationComment");

            migrationBuilder.DropTable(
                name: "PublicationLike");

            migrationBuilder.DropTable(
                name: "PublicationTag");

            migrationBuilder.DropTable(
                name: "PublicationView");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
