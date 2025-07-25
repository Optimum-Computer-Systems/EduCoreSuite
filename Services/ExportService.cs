using EduCoreSuite.Data;
using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text;

namespace EduCoreSuite.Services
{
    public class ExportService
    {
        private readonly ApplicationDbContext _context;

        public ExportService(ApplicationDbContext context)
        {
            _context = context;
            // Ensure EPPlus license is set
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<byte[]> ExportStudentsToExcel(string? searchTerm = null, string? departmentFilter = null, string? programmeFilter = null)
        {
            var studentsQuery = _context.Students
                .Include(s => s.County)
                .Include(s => s.SubCounty)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                studentsQuery = studentsQuery.Where(s => 
                    s.FullName.Contains(searchTerm) ||
                    s.AdmissionNumber.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(departmentFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Department == departmentFilter);
            }

            if (!string.IsNullOrWhiteSpace(programmeFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Program == programmeFilter);
            }

            var students = await studentsQuery.OrderBy(s => s.FullName).ToListAsync();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students Report");

            // Headers
            var headers = new[]
            {
                "Admission Number", "Full Name", "Email", "Phone Number", "Department", 
                "Course", "Programme", "Year", "Gender", "ID Number",
                "County", "Sub-County", "Emergency Contact", "Emergency Phone"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Style headers
            using (var range = worksheet.Cells[1, 1, 1, headers.Length])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                range.Style.Border.BorderAround(ExcelBorderStyle.Thick);
            }

            // Data
            for (int i = 0; i < students.Count; i++)
            {
                var student = students[i];
                var row = i + 2;

                worksheet.Cells[row, 1].Value = student.AdmissionNumber;
                worksheet.Cells[row, 2].Value = student.FullName;
                worksheet.Cells[row, 3].Value = student.Email;
                worksheet.Cells[row, 4].Value = student.PhoneNumber ?? "";
                worksheet.Cells[row, 5].Value = student.Department ?? "";
                worksheet.Cells[row, 6].Value = student.Course ?? "";
                worksheet.Cells[row, 7].Value = student.Program ?? "";
                worksheet.Cells[row, 8].Value = student.Year;
                worksheet.Cells[row, 9].Value = student.Gender ?? "";
                worksheet.Cells[row, 10].Value = student.IDNumber ?? "";
                worksheet.Cells[row, 11].Value = student.County?.CountyName ?? "";
                worksheet.Cells[row, 12].Value = student.SubCounty?.SubCountyName ?? "";
                worksheet.Cells[row, 13].Value = student.EmergencyName ?? "";
                worksheet.Cells[row, 14].Value = student.EmergencyPhone ?? "";
            }

            worksheet.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }

        public async Task<string> ExportStudentsToCsv(string? searchTerm = null, string? departmentFilter = null, string? programmeFilter = null)
        {
            var studentsQuery = _context.Students
                .Include(s => s.County)
                .Include(s => s.SubCounty)
                .AsQueryable();

            // Apply filters (same as Excel export)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                studentsQuery = studentsQuery.Where(s => 
                    s.FullName.Contains(searchTerm) ||
                    s.AdmissionNumber.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(departmentFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Department == departmentFilter);
            }

            if (!string.IsNullOrWhiteSpace(programmeFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.Program == programmeFilter);
            }

            var students = await studentsQuery.OrderBy(s => s.FullName).ToListAsync();

            var csv = new StringBuilder();
            
            // Headers
            csv.AppendLine("Admission Number,Full Name,Email,Phone Number,Department,Course,Programme,Year,Gender,ID Number,County,Sub-County,Emergency Contact,Emergency Phone");

            // Data
            foreach (var student in students)
            {
                csv.AppendLine($"\"{student.AdmissionNumber}\",\"{student.FullName}\",\"{student.Email ?? ""}\",\"{student.PhoneNumber ?? ""}\",\"{student.Department ?? ""}\",\"{student.Course ?? ""}\",\"{student.Program ?? ""}\",\"{student.Year}\",\"{student.Gender ?? ""}\",\"{student.IDNumber ?? ""}\",\"{student.County?.CountyName ?? ""}\",\"{student.SubCounty?.SubCountyName ?? ""}\",\"{student.EmergencyName ?? ""}\",\"{student.EmergencyPhone ?? ""}\"");
            }

            return csv.ToString();
        }
    }
}