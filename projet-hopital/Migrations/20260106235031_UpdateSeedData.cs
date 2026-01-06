using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_hopital.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 450.00m, "Yassine", "https://images.unsplash.com/photo-1700041785712-649e859d9909?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Lechgar" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 375.00m, "Amal", "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Alawi" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 400.00m, "Anas", "https://images.unsplash.com/photo-1622253692010-333f2da6031d?q=80&w=764&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Debagh" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConsultationFee", "FirstName", "LastName" },
                values: new object[] { 500.00m, "Manar", "Sefawi" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 420.00m, "Ali", "https://plus.unsplash.com/premium_photo-1723780925032-1ad5b130db77?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Lakhder" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 380.00m, "Dawoud", "https://images.unsplash.com/photo-1582750433449-648ed127bb54?q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Mountasib" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://www.ramsayservices.fr/sites/default/files/styles/original/public/2025-05/iStock-1464012950%20%281%29.jpg?itok=nrlgm6-M", 375.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://www.medicaim.com/uploads/84ed39b0-5a46-11e8-8fc5-bf61b50ce761/echocardiogramme.jpg", 550.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://sf1.topsante.com/wp-content/uploads/topsante/2023/09/maladies-coeur-choses-savoir-sur-test-effort.jpeg", 600.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://bolgehospitalinternational.com/wp-content/uploads/2023/11/EEG.jpg", 500.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://atlantabrainandspine.com/wp-content/uploads/2022/02/iStock-1254897003.jpg", 550.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://www.solimut-mutuelle.fr/wp-content/uploads/2024/08/Format-670x410-1.jpg", 480.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://blogs.worldbank.org/content/dam/sites/blogs/img/detail/mgr/vaccination.png", 450.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.philips.com/is/image/philipsconsumer/8801433f55664de7a17bb11400ea0b8a?extend=0,-280,0,-575&wid=1500&hei=844&fit=stretch,1&$jpglarge$", 500.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://www.centremjmedical.com/wp-content/uploads/2022/05/Medecine-Douce-Kinesitherapie-Centre-MJ-Medical-Marrakech-1.jpg", 520.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.ctfassets.net/nla4ils4bv6t/ZrA1T29UE5kaUfJhVuRjo/3907d77dae1904df0e77002f2b812aba/dermato.jpg", 590.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://allure-derm.com/storage/2024/04/Acne-Treatment-by-Allure-Dermatology-in-Edinburg-TX.webp", 550.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://www.ecotowndiagnostics.com/wp-content/uploads/2024/06/818dce83d6.jpg", 560.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DurationMinutes", "ImageUrl" },
                values: new object[] { 600, "https://myhealthassistance.ma/wp-content/uploads/2024/04/BandeauBilanSante-1_0.jpg" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://cdn8.futura-sciences.com/a1280/images/prise%20de%20sang.jpg", 550.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 150.00m, "Jean", "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300", "Martin" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 175.00m, "Marie", "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=300", "Dupont" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 100.00m, "Pierre", "https://images.unsplash.com/photo-1622253692010-333f2da6031d?w=300", "Bernard" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConsultationFee", "FirstName", "LastName" },
                values: new object[] { 200.00m, "Sophie", "Leroy" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 120.00m, "Antoine", "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=300", "Moreau" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConsultationFee", "FirstName", "ImageUrl", "LastName" },
                values: new object[] { 80.00m, "Claire", "https://images.unsplash.com/photo-1582750433449-648ed127bb54?w=300", "Petit" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1559757148-5c350d0d3c56?w=400", 75.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1516549655169-df83a0774514?w=400", 250.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1581594693702-fbdc51b2763b?w=400", 300.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1530497610245-94d3c16cda28?w=400", 200.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=400", 150.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1551076805-e1869033e561?w=400", 80.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=400", 50.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1628348068343-c6a848d2b6dd?w=400", 100.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=400", 120.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?w=400", 90.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1570172619644-dfd03ed5d881?w=400", 150.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1666214280557-f1b5022eb634?w=400", 60.00m });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DurationMinutes", "ImageUrl" },
                values: new object[] { 60, "https://images.unsplash.com/photo-1584982751601-97dcc096659c?w=400" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ImageUrl", "Price" },
                values: new object[] { "https://images.unsplash.com/photo-1579154204601-01588f351e67?w=400", 150.00m });
        }
    }
}
