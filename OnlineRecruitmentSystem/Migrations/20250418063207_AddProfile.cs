using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRecruitmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "UserBio");

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollegeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CGPA = table.Column<double>(type: "float", nullable: false),
                    Achievements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResumeFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");

            //migrationBuilder.CreateTable(
            //    name: "UserBio",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Achievements = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
            //        CGPA = table.Column<double>(type: "float", nullable: false),
            //        CollegeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Degree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ResumeFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Skills = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
            //        UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserBio", x => x.Id);
            //    });
        }
    }
}
