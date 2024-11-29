using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_bookStore.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth",
                columns: table => new
                {
                    auth_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    auth_user_id = table.Column<string>(type: "varchar(60)", nullable: false),
                    auth_user_email = table.Column<string>(type: "varchar(60)", nullable: false),
                    auth_user_password = table.Column<string>(type: "varchar(255)", nullable: false),
                    auth_user_token = table.Column<string>(type: "varchar(60)", nullable: false),
                    auth_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    auth_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth", x => x.auth_id);
                });

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    author_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    author_birthday = table.Column<DateOnly>(type: "Date", nullable: false),
                    author_country = table.Column<int>(type: "int", nullable: false),
                    author_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    author_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "inventory_book",
                columns: table => new
                {
                    inventory_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inventory_value = table.Column<double>(type: "float", nullable: false),
                    inventory_quantity = table.Column<int>(type: "int", nullable: false),
                    inventory_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    inventory_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_book", x => x.inventory_id);
                });

            migrationBuilder.CreateTable(
                name: "sale",
                columns: table => new
                {
                    sale_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sale_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale", x => x.sale_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "varchar(60)", nullable: false),
                    user_email = table.Column<string>(type: "varchar(60)", nullable: false),
                    user_password = table.Column<string>(type: "varchar(255)", nullable: false),
                    user_role = table.Column<int>(type: "int", nullable: false),
                    user_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false),
                    book_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    book_publicationDate = table.Column<DateOnly>(type: "Date", nullable: false),
                    book_price = table.Column<double>(type: "float", nullable: false),
                    book_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    book_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    book_authorId = table.Column<int>(type: "int", nullable: false),
                    book_categoryId = table.Column<int>(type: "int", nullable: false),
                    book_inventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_book_author_book_authorId",
                        column: x => x.book_authorId,
                        principalTable: "author",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_category_book_categoryId",
                        column: x => x.book_categoryId,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_inventory_book_book_id",
                        column: x => x.book_id,
                        principalTable: "inventory_book",
                        principalColumn: "inventory_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_x_book",
                columns: table => new
                {
                    sale_book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sale_price = table.Column<double>(type: "float", nullable: false),
                    sale_quantity = table.Column<int>(type: "int", nullable: false),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_x_book", x => x.sale_book_id);
                    table.ForeignKey(
                        name: "FK_sale_x_book_book_book_id",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_x_book_sale_sale_id",
                        column: x => x.sale_id,
                        principalTable: "sale",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_book_book_authorId",
                table: "book",
                column: "book_authorId");

            migrationBuilder.CreateIndex(
                name: "IX_book_book_categoryId",
                table: "book",
                column: "book_categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_x_book_book_id",
                table: "sale_x_book",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_x_book_sale_id",
                table: "sale_x_book",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_email",
                table: "user",
                column: "user_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth");

            migrationBuilder.DropTable(
                name: "sale_x_book");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "sale");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "inventory_book");
        }
    }
}
