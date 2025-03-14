﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementWithAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TicketId",
                table: "Notifications",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Tickets_TicketId",
                table: "Notifications",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Tickets_TicketId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_TicketId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Notifications");
        }
    }
}
