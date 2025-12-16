using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIdProofToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProofPath",
                table: "Students",
                newName: "IdProofFileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "IdProof",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "IdProofContentType",
                table: "Students",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProof",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IdProofContentType",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "IdProofFileName",
                table: "Students",
                newName: "IdProofPath");
        }
    }
}
