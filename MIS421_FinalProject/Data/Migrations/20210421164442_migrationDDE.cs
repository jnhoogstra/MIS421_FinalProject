using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS421_FinalProject.Data.Migrations
{
    public partial class migrationDDE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    deptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deptAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deptCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deptCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deptState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deptZip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.deptID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    empID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empBDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    empHireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfilePic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    empPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empZip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentdeptID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.empID);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentdeptID",
                        column: x => x.DepartmentdeptID,
                        principalTable: "Department",
                        principalColumn: "deptID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    docID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    docName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    verified = table.Column<bool>(type: "bit", nullable: false),
                    content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmployeeempID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.docID);
                    table.ForeignKey(
                        name: "FK_Document_Employee_EmployeeempID",
                        column: x => x.EmployeeempID,
                        principalTable: "Employee",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_EmployeeempID",
                table: "Document",
                column: "EmployeeempID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentdeptID",
                table: "Employee",
                column: "DepartmentdeptID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
