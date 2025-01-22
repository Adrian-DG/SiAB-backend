using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SiAB.API.Attributes;

namespace SiAB.API.Controllers.Reports
{
	[Authorize]	
	[ApiController]
	[AvoidCustomResponse]
	[Route("api/excel-templates")]	
	public class ExcelTemplateController : ControllerBase
	{
		public ExcelTemplateController()
		{
		}

		[HttpGet("plantilla-relacion-armas-militares")]
		public IActionResult GetPlantillaRelacionArmasMilitares()
		{
			using (ExcelPackage excel = new ExcelPackage())
			{				
				var workSheet = excel.Workbook.Worksheets.Add("relacion #1");

				// Header

				var columns = new string[] { "cedula", "tipo", "marca", "modelo", "calibre", "serie", "propiedad", "formulario53", "fechaEfectividad" };
				var header = workSheet.Cells[1, 1, 1, columns.Length];

				for (var i = 0; i < columns.Length; i++)
				{
					workSheet.Cells[1, i + 1].Value = columns[i];
				}

				header.Style.Font.Bold = true;
				header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

				using (MemoryStream ms = new MemoryStream()) 
				{ 
					excel.SaveAs(ms);
					byte[] file = ms.ToArray();
					return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "plantilla_relacion_armas_militares.xlsx");
				}
			}
		}

		[HttpGet("plantilla-relacion-armas-civiles")]
		public IActionResult GetPlantillaRelacionArmasCiviles()
		{
			using (ExcelPackage excel = new ExcelPackage())
			{
				var workSheet = excel.Workbook.Worksheets.Add("relacion #1");

				// Header

				var columns = new string[] { "cedula", "tipo", "marca", "modelo", "calibre", "serie", "propiedad", "formulario53", "fechaEfectividad" };
				var header = workSheet.Cells[1, 1, 1, columns.Length];

				for (var i = 0; i < columns.Length; i++)
				{
					workSheet.Cells[1, i + 1].Value = columns[i];
				}

				header.Style.Font.Bold = true;
				header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

				using (MemoryStream ms = new MemoryStream())
				{
					excel.SaveAs(ms);
					byte[] file = ms.ToArray();
					return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "plantilla_relacion_armas_civiles.xlsx");
				}
			}
		}

		[HttpGet("plantilla-relacion-armas-dependencias")]
		public IActionResult GetPlantillaRelacionArmasDependencias()
		{
			using (ExcelPackage excel = new ExcelPackage())
			{
				var workSheet = excel.Workbook.Worksheets.Add("relacion #1");

				// Header

				var columns = new string[] { "cedula", "tipo", "marca", "modelo", "calibre", "serie", "propiedad", "formulario53", "fechaEfectividad" };
				var header = workSheet.Cells[1, 1, 1, columns.Length];

				for (var i = 0; i < columns.Length; i++)
				{
					workSheet.Cells[1, i + 1].Value = columns[i];
				}

				header.Style.Font.Bold = true;
				header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

				using (MemoryStream ms = new MemoryStream())
				{
					excel.SaveAs(ms);
					byte[] file = ms.ToArray();
					return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "plantilla_relacion_armas_dependencias.xlsx");
				}
			}
		}

	}
}
