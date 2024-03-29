﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer4B.Server.Data.Migrations.IdentityServer.PersistedGrant
{
    public partial class InitAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__IdentityServerPersistedGrant__DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerPersistedGrant__DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "__IdentityServerPersistedGrant__PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___IdentityServerPersistedGrant__PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerPersistedGrant__DeviceCodes_DeviceCode",
                table: "__IdentityServerPersistedGrant__DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerPersistedGrant__DeviceCodes_Expiration",
                table: "__IdentityServerPersistedGrant__DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerPersistedGrant__PersistedGrants_Expiration",
                table: "__IdentityServerPersistedGrant__PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerPersistedGrant__PersistedGrants_SubjectId_ClientId_Type",
                table: "__IdentityServerPersistedGrant__PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX___IdentityServerPersistedGrant__PersistedGrants_SubjectId_SessionId_Type",
                table: "__IdentityServerPersistedGrant__PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__IdentityServerPersistedGrant__DeviceCodes");

            migrationBuilder.DropTable(
                name: "__IdentityServerPersistedGrant__PersistedGrants");
        }
    }
}
