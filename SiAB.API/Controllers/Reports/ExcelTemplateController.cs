﻿using Microsoft.AspNetCore.Authorization;
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


		[HttpGet("download-plantilla-relacion-armas")]
		public IActionResult GetPlantillaRelacionArmasMilitares([FromQuery] string origen)
		{
			using (ExcelPackage excel = new ExcelPackage())
			{				
				var workSheet = excel.Workbook.Worksheets.Add("relacion #1");

				// Header

				var columns = new string[] { "origen (cedula o deposito)", "destino (cedula o deposito)", "categoria", "tipo", "subtipo", "marca", "modelo", "calibre", "serie", "formulario53", "fechaEfectividad" };
				var header = workSheet.Cells[1, 1, 1, columns.Length];

				for (var i = 0; i < columns.Length; i++)
				{
					workSheet.Cells[1, i + 1].Value = columns[i];

					if (columns[i] == "fechaEfectividad")
					{
						workSheet.Column(i + 1).Style.Numberformat.Format = "dd/MM/yyyy";
					}
				}

				header.Style.Font.Bold = true;
				header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

				// Institucion
				workSheet.Cells[2, 1].Value = "MIDE";

				// Dependencia
				workSheet.Cells[2, 2].Value = origen;

				using (MemoryStream ms = new MemoryStream()) 
				{ 
					excel.SaveAs(ms);
					byte[] file = ms.ToArray();
					return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "plantilla_relacion_armas_militares.xlsx");
				}
			}
		}
		

	}
}
