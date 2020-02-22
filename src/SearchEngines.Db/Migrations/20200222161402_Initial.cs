using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchEngines.Db.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchText = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchSystems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchResponses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    HasError = table.Column<bool>(nullable: false),
                    SearchRequestId = table.Column<long>(nullable: true),
                    SearchSystemId = table.Column<long>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResponses_SearchRequests_SearchRequestId",
                        column: x => x.SearchRequestId,
                        principalTable: "SearchRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SearchResponses_SearchSystems_SearchSystemId",
                        column: x => x.SearchSystemId,
                        principalTable: "SearchSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLinkText = table.Column<string>(maxLength: 1000, nullable: true),
                    Url = table.Column<string>(maxLength: 2000, nullable: true),
                    PreviewData = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SearchResponseId = table.Column<long>(nullable: true),
                    SearchRequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResults_SearchRequests_SearchRequestId",
                        column: x => x.SearchRequestId,
                        principalTable: "SearchRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SearchResults_SearchResponses_SearchResponseId",
                        column: x => x.SearchResponseId,
                        principalTable: "SearchResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchResponses_SearchRequestId",
                table: "SearchResponses",
                column: "SearchRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResponses_SearchSystemId",
                table: "SearchResponses",
                column: "SearchSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_SearchRequestId",
                table: "SearchResults",
                column: "SearchRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_SearchResponseId",
                table: "SearchResults",
                column: "SearchResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResults");

            migrationBuilder.DropTable(
                name: "SearchResponses");

            migrationBuilder.DropTable(
                name: "SearchRequests");

            migrationBuilder.DropTable(
                name: "SearchSystems");
        }
    }
}
