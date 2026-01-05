using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_hopital.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToFrenchWithOnlineImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Soins du cœur et du système cardiovasculaire", "https://images.unsplash.com/photo-1628348068343-c6a848d2b6dd?w=300", "Cardiologie" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Traitement du cerveau et du système nerveux", "https://images.unsplash.com/photo-1559757175-5700dde675bc?w=300", "Neurologie" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Soins médicaux pour les nourrissons, enfants et adolescents", "https://images.unsplash.com/photo-1581594693702-fbdc51b2763b?w=300", "Pédiatrie" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Traitement du système musculo-squelettique", "https://images.unsplash.com/photo-1579684453423-f84349ef60b0?w=300", "Orthopédie" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Soins de la peau, des cheveux et des ongles", "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300", "Dermatologie" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Soins de santé primaires et services médicaux généraux", "https://images.unsplash.com/photo-1666214280557-f1b5022eb634?w=300", "Médecine Générale" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Expert en maladies cardiovasculaires avec 15 ans d'expérience", "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300", "Cardiologue" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Spécialisée dans les troubles neurologiques et la santé cérébrale", "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=300", "Neurologue" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Dédié à la santé et au bien-être des enfants", "https://images.unsplash.com/photo-1622253692010-333f2da6031d?w=300", "Pédiatre" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Expert en chirurgie osseuse et articulaire", "https://images.unsplash.com/photo-1594824476967-48c8b964273f?w=300", "Chirurgien Orthopédiste" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Spécialiste des soins de la peau avec expertise dans diverses affections cutanées", "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=300", "Dermatologue" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Expert en médecine familiale offrant des soins primaires complets", "https://images.unsplash.com/photo-1582750433449-648ed127bb54?w=300", "Médecin Généraliste" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Enregistre l'activité électrique du cœur", "https://images.unsplash.com/photo-1559757148-5c350d0d3c56?w=400", "ECG - Électrocardiogramme" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Imagerie par ultrasons du cœur", "https://images.unsplash.com/photo-1516549655169-df83a0774514?w=400", "Échocardiogramme" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Évalue la fonction cardiaque pendant l'activité physique", "https://images.unsplash.com/photo-1581594693702-fbdc51b2763b?w=400", "Test d'Effort Cardiaque" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Mesure l'activité électrique du cerveau", "https://images.unsplash.com/photo-1530497610245-94d3c16cda28?w=400", "EEG - Électroencéphalogramme" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Évaluation complète de la fonction du système nerveux", "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=400", "Examen Neurologique" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Examen de santé de routine pour les enfants", "https://images.unsplash.com/photo-1551076805-e1869033e561?w=400", "Bilan de Santé Enfant" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Services d'immunisation pour les enfants", "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=400" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Imagerie diagnostique pour les os et articulations", "https://images.unsplash.com/photo-1628348068343-c6a848d2b6dd?w=400", "Radiographie" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Rééducation et exercices thérapeutiques", "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=400", "Séance de Kinésithérapie" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Évaluation complète de la santé de la peau", "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?w=400", "Examen Dermatologique" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Gestion et traitement professionnel de l'acné", "https://images.unsplash.com/photo-1570172619644-dfd03ed5d881?w=400", "Traitement de l'Acné" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Consultation de soins primaires et évaluation de santé", "https://images.unsplash.com/photo-1666214280557-f1b5022eb634?w=400", "Consultation Générale" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Examen de santé annuel complet", "https://images.unsplash.com/photo-1584982751601-97dcc096659c?w=400", "Bilan de Santé Annuel" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Numération formule sanguine complète et panel chimique", "https://images.unsplash.com/photo-1579154204601-01588f351e67?w=400", "Bilan Sanguin Complet" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Heart and cardiovascular system care", "/images/departments/cardiology.jpg", "Cardiology" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Brain and nervous system treatment", "/images/departments/neurology.jpg", "Neurology" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Medical care for infants, children, and adolescents", "/images/departments/pediatrics.jpg", "Pediatrics" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Musculoskeletal system treatment", "/images/departments/orthopedics.jpg", "Orthopedics" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Skin, hair, and nail care", "/images/departments/dermatology.jpg", "Dermatology" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Primary healthcare and general medical services", "/images/departments/general.jpg", "General Medicine" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Expert in cardiovascular diseases with 15 years of experience", "/images/doctors/doctor-1.jpg", "Cardiologist" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Specialized in neurological disorders and brain health", "/images/doctors/doctor-2.jpg", "Neurologist" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Dedicated to children's health and wellness", "/images/doctors/doctor-3.jpg", "Pediatrician" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Expert in bone and joint surgery", "/images/doctors/doctor-4.jpg", "Orthopedic Surgeon" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Skin care specialist with expertise in various skin conditions", "/images/doctors/doctor-5.jpg", "Dermatologist" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Bio", "ImageUrl", "Specialization" },
                values: new object[] { "Family medicine expert providing comprehensive primary care", "/images/doctors/doctor-6.jpg", "General Practitioner" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Records the electrical activity of the heart", "/images/services/ecg.jpg", "ECG - Electrocardiogram" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Ultrasound imaging of the heart", "/images/services/echo.jpg", "Echocardiogram" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Evaluates heart function during physical activity", "/images/services/stress-test.jpg", "Cardiac Stress Test" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Measures electrical activity in the brain", "/images/services/eeg.jpg", "EEG - Electroencephalogram" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Comprehensive assessment of nervous system function", "/images/services/neuro-exam.jpg", "Neurological Examination" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Routine health examination for children", "/images/services/child-checkup.jpg", "Well-Child Checkup" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Immunization services for children", "/images/services/vaccination.jpg" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Diagnostic imaging for bones and joints", "/images/services/xray.jpg", "X-Ray Imaging" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Rehabilitation and therapeutic exercises", "/images/services/physical-therapy.jpg", "Physical Therapy Session" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Comprehensive skin health assessment", "/images/services/skin-exam.jpg", "Skin Examination" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Professional acne management and treatment", "/images/services/acne-treatment.jpg", "Acne Treatment" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Primary care consultation and health assessment", "/images/services/consultation.jpg", "General Consultation" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Comprehensive yearly health examination", "/images/services/health-checkup.jpg", "Annual Health Checkup" });

            migrationBuilder.UpdateData(
                table: "MedicalServices",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Complete blood count and chemistry panel", "/images/services/blood-test.jpg", "Blood Test Panel" });
        }
    }
}
