using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingPortal.EntityFrameWorkCore.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.EnsureSchema(
                name: "user_management");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    ProfilePhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationRequestResponse",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationRequestResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "user_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "user_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "user_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TrackingCorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adress",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adress_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "dbo",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "user_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "user_management",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user_management",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "user_management",
                table: "Roles",
                columns: new[] { "Id", "Name", "NameAr" },
                values: new object[,]
                {
                    { 1, "admin", "مدير النظام" },
                    { 2, "user", "مستخدم" }
                });

            migrationBuilder.InsertData(
                schema: "user_management",
                table: "Users",
                columns: new[] { "Id", "Password", "PasswordSalt", "TrackingCorrelationId", "UserName" },
                values: new object[,]
                {
                    { 1, new byte[] { 85, 135, 159, 29, 84, 88, 175, 93, 155, 199, 236, 114, 173, 245, 115, 176, 125, 230, 91, 162, 186, 142, 24, 215, 61, 96, 81, 44, 202, 79, 182, 38, 52, 126, 188, 203, 188, 87, 126, 24, 36, 191, 82, 234, 238, 235, 48, 18, 58, 130, 15, 57, 141, 134, 173, 100, 60, 107, 244, 123, 70, 254, 62, 230 }, new byte[] { 42, 206, 208, 223, 244, 163, 70, 59, 250, 198, 218, 17, 90, 192, 244, 187, 190, 232, 45, 112, 153, 183, 176, 6, 222, 67, 173, 103, 55, 40, 4, 79, 26, 125, 128, 120, 149, 145, 14, 120, 45, 166, 150, 184, 194, 224, 100, 160, 82, 170, 242, 220, 13, 21, 116, 61, 184, 237, 221, 61, 1, 46, 116, 145, 148, 54, 236, 209, 88, 173, 255, 249, 231, 138, 171, 40, 23, 4, 226, 73, 131, 176, 218, 31, 155, 248, 53, 46, 102, 102, 242, 78, 201, 199, 19, 225, 169, 181, 19, 250, 198, 249, 17, 156, 163, 127, 142, 98, 135, 215, 182, 145, 80, 54, 184, 11, 211, 34, 253, 65, 44, 206, 27, 134, 144, 163, 111, 225 }, new Guid("00000000-0000-0000-0000-000000000000"), "admin" },
                    { 2, new byte[] { 86, 221, 239, 27, 80, 138, 102, 47, 104, 44, 91, 114, 238, 14, 230, 29, 53, 24, 143, 75, 188, 42, 169, 149, 97, 221, 34, 17, 179, 153, 176, 94, 196, 57, 94, 108, 143, 113, 214, 121, 88, 58, 18, 40, 53, 226, 202, 254, 45, 237, 8, 5, 86, 234, 66, 215, 211, 228, 116, 15, 45, 180, 104, 186 }, new byte[] { 71, 175, 219, 230, 136, 96, 241, 38, 62, 197, 8, 67, 196, 57, 161, 167, 45, 219, 13, 121, 180, 40, 234, 242, 3, 21, 47, 230, 20, 78, 68, 189, 73, 128, 81, 50, 214, 82, 232, 139, 165, 225, 54, 127, 212, 13, 195, 68, 82, 145, 93, 157, 173, 1, 25, 157, 57, 253, 233, 205, 90, 79, 93, 179, 190, 228, 185, 127, 174, 130, 68, 192, 247, 60, 65, 136, 32, 9, 249, 141, 55, 129, 63, 129, 83, 32, 30, 103, 196, 121, 39, 134, 253, 180, 108, 84, 114, 237, 145, 222, 62, 98, 222, 237, 239, 57, 137, 114, 156, 163, 144, 196, 11, 67, 221, 239, 179, 199, 82, 54, 155, 175, 111, 12, 219, 28, 159, 10 }, new Guid("00000000-0000-0000-0000-000000000000"), "user" }
                });

            migrationBuilder.InsertData(
                schema: "user_management",
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ClientId",
                schema: "dbo",
                table: "Account",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Adress_ClientId",
                schema: "dbo",
                table: "Adress",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                schema: "dbo",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "user_management",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "user_management",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Adress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntegrationRequestResponse",
                schema: "log");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "user_management");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "user_management");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "user_management");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "user_management");
        }
    }
}
