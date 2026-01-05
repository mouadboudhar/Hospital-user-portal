using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_hopital.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/doctors/doctor-1.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/doctors/doctor-2.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/doctors/doctor-3.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/doctors/doctor-4.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/doctors/doctor-5.jpg");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/doctors/doctor-6.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/services/ecg.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/services/echo.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/services/stress-test.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/services/eeg.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/services/neuro-exam.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/services/child-checkup.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/images/services/vaccination.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/images/services/xray.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/services/physical-therapy.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/services/skin-exam.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "/images/services/acne-treatment.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "/images/services/consultation.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "/images/services/health-checkup.jpg");

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "/images/services/blood-test.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: null);
        }
    }
}
