using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetLinkShortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIpToLinkView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "LinkViews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "LinkViews");
        }
    }
}
