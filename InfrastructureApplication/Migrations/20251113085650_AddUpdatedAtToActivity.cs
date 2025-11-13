using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "querson");

            migrationBuilder.CreateTable(
                name: "crm_customers",
                schema: "querson",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalErpId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "crm_products",
                schema: "querson",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalErpId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "crm_synclog",
                schema: "querson",
                columns: table => new
                {
                    SyncId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ExternalErpId = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_synclog", x => x.SyncId);
                });

            migrationBuilder.CreateTable(
                name: "crm_activities",
                schema: "querson",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_crm_activities_crm_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "querson",
                        principalTable: "crm_customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crm_orders",
                schema: "querson",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalErpId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_crm_orders_crm_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "querson",
                        principalTable: "crm_customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crm_financialtransactions",
                schema: "querson",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalErpId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_financialtransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_crm_financialtransactions_crm_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "querson",
                        principalTable: "crm_customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crm_financialtransactions_crm_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "querson",
                        principalTable: "crm_orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "crm_orderitems",
                schema: "querson",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_orderitems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_crm_orderitems_crm_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "querson",
                        principalTable: "crm_orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crm_orderitems_crm_products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "querson",
                        principalTable: "crm_products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_crm_activities_CustomerId",
                schema: "querson",
                table: "crm_activities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_crm_financialtransactions_CustomerId",
                schema: "querson",
                table: "crm_financialtransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_crm_financialtransactions_OrderId",
                schema: "querson",
                table: "crm_financialtransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_crm_orderitems_OrderId",
                schema: "querson",
                table: "crm_orderitems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_crm_orderitems_ProductId",
                schema: "querson",
                table: "crm_orderitems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_crm_orders_CustomerId",
                schema: "querson",
                table: "crm_orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crm_activities",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_financialtransactions",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_orderitems",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_synclog",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_orders",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_products",
                schema: "querson");

            migrationBuilder.DropTable(
                name: "crm_customers",
                schema: "querson");
        }
    }
}
