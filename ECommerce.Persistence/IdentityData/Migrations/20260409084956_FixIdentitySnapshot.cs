using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Persistence.IdentityData.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentitySnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Intentionally empty — fixes polluted Identity model snapshot only.
            // Product/Order/DeliveryMethod tables never belonged to this context.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally empty
        }
    }
}