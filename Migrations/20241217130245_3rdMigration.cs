using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_solar.Migrations
{
    /// <inheritdoc />
    public partial class _3rdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureSensorData",
                table: "TemperatureSensorData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarPanelData",
                table: "SolarPanelData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LuminositySensorData",
                table: "LuminositySensorData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirtSensorData",
                table: "DirtSensorData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureSensorData",
                table: "TemperatureSensorData",
                columns: new[] { "Id", "Timestamp" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarPanelData",
                table: "SolarPanelData",
                columns: new[] { "Id", "Timestamp" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LuminositySensorData",
                table: "LuminositySensorData",
                columns: new[] { "Id", "Timestamp" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirtSensorData",
                table: "DirtSensorData",
                columns: new[] { "Id", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureSensorData",
                table: "TemperatureSensorData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarPanelData",
                table: "SolarPanelData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LuminositySensorData",
                table: "LuminositySensorData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirtSensorData",
                table: "DirtSensorData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureSensorData",
                table: "TemperatureSensorData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarPanelData",
                table: "SolarPanelData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LuminositySensorData",
                table: "LuminositySensorData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirtSensorData",
                table: "DirtSensorData",
                column: "Id");
        }
    }
}
