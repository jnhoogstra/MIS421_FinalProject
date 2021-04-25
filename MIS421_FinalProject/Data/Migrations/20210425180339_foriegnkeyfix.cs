using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS421_FinalProject.Data.Migrations
{
    public partial class foriegnkeyfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Employee_EmployeeempID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentdeptID",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "DepartmentdeptID",
                table: "Employee",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_DepartmentdeptID",
                table: "Employee",
                newName: "IX_Employee_DepartmentID");

            migrationBuilder.RenameColumn(
                name: "EmployeeempID",
                table: "Document",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_EmployeeempID",
                table: "Document",
                newName: "IX_Document_EmployeeID");

            migrationBuilder.AddColumn<int>(
                name: "deptID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "empID",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Employee_EmployeeID",
                table: "Document",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "empID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentID",
                table: "Employee",
                column: "DepartmentID",
                principalTable: "Department",
                principalColumn: "deptID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Employee_EmployeeID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "deptID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "empID",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Employee",
                newName: "DepartmentdeptID");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_DepartmentID",
                table: "Employee",
                newName: "IX_Employee_DepartmentdeptID");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Document",
                newName: "EmployeeempID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_EmployeeID",
                table: "Document",
                newName: "IX_Document_EmployeeempID");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Employee_EmployeeempID",
                table: "Document",
                column: "EmployeeempID",
                principalTable: "Employee",
                principalColumn: "empID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentdeptID",
                table: "Employee",
                column: "DepartmentdeptID",
                principalTable: "Department",
                principalColumn: "deptID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
