using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SiAB.API.Controllers.Belico;
using SiAB.API.Filters;
using SiAB.API.Helpers;
using SiAB.Application.Contracts;
using SiAB.Core.Abstraction;
using SiAB.Core.Enums;
using System.Drawing;

namespace SiAB.API.Controllers.Reports
{
	[Route("api/reports")]
	[TypeFilter(typeof(CodUsuarioFilter))]
	[TypeFilter(typeof(CodInstitucionFilter))]
	public class GenericReportsController : GenericController<EntityMetadata>
	{
		private readonly TransaccionesController _transaccionesCtrl;
		private readonly IDatabaseConnectionService _connectionService;
		public GenericReportsController(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService, IDatabaseConnectionService connectionService) : base(unitOfWork, mapper, userContextService)
		{
			_connectionService = connectionService;
		}

		[AllowAnonymous]
		[HttpGet("miembro/historial-armas-cargadas")]
		public async Task GetHistorialArmasCargadas([FromQuery] string cedula)
		{
			QuestPDF.Settings.License = LicenseType.Community;

			var miembro = await _uow.SipffaaRepository.GetMiembroByCedula(cedula);			

			if (miembro is null) return;

			var foto = await _uow.SipffaaRepository.GetMiembroFoto(cedula);

			var image = System.IO.File.ReadAllBytes(Path.Combine("Images", "Mini_Logo_Principal_MIDE.png"));;

			await Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(1, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(12));

					page.Header()
					.Column(c =>
					{
						c.Item()
						.AlignCenter()
						.Width(150)
						.Height(100)
						.Image(image);

						c.Item()
						.Text("Historial de armas cargadas")
						.AlignCenter()
						.FontSize(20)
						.FontFamily("Arial")
						.FontColor(Colors.Black)
						.Bold();
					});

					page.Content()
					.PaddingVertical(1, Unit.Centimetre)
					.Column(c =>
					{

						c.Item()
						.Row(r =>
						 {
							 r.AutoItem()
							 .Column(c =>
							 {
								 c.Item()
								 .Width(150)
								 .Height(150)
								 .Image(foto);
							 });

							 r.AutoItem()
							 .Width(340)
							 .Height(150)
							 .PaddingLeft(10)
							 .Background(Colors.Grey.Lighten3)
							 .Column(c =>
							 {
								 c.Spacing(5);

								 c.Item()
								 .PaddingTop(10)
								 .PaddingLeft(10)
								 .Text(t =>
								 {
									 t.Span("Rango: ")
									 .Bold();

									 t.Span(miembro.Rango);
								 });

								 if (!String.IsNullOrEmpty(miembro.Profesion))
								 {
									 c.Item()
									 .PaddingVertical(5)
									 .PaddingLeft(10)
									 .Text(t =>
									 {
										 t.Span("Profesió: ")
										 .Bold();

										 t.Span(miembro.Profesion);
									 });
								 }

								 c.Item()
								 .PaddingVertical(5)
								 .PaddingLeft(10)
								 .Text(t =>
								 {
									 t.Span("Cédula: ")
									 .Bold();

									 t.Span(miembro.Cedula);
								 });

								 c.Item()
								 .PaddingVertical(5)
								 .PaddingLeft(10)
								 .Text(t =>
								 {
									 t.Span("Nombre Completo: ")
									 .Bold();

									 t.Span(miembro.NombreApellidoCompleto);
								 });
							 });
						 });

						c.Item().PaddingTop(20)
						.Column(c =>
						{
							c.Item()
							.AlignLeft()
							.Text("Historico")
							.Bold();

							c.Item()
							.Table(t =>
							{
								int TABLE_PADDING = 5;
								int BORDER_WIDTH = 2;

								t.ColumnsDefinition(d =>
								{
									d.RelativeColumn();
									d.RelativeColumn();
									d.RelativeColumn();
								});

								t.Header(header =>
								{
									header.Cell().BorderBottom(BORDER_WIDTH).Padding(TABLE_PADDING).Text("Serie");
									header.Cell().BorderBottom(BORDER_WIDTH).Padding(TABLE_PADDING).Text("Fecha");
								});

								foreach (var i in Enumerable.Range(0, 6))
								{
									t.Cell().BorderBottom(BORDER_WIDTH).Padding(TABLE_PADDING).Text(i.ToString());							
								}

							});

						});

					});
					
					

					page.Footer()
						.AlignCenter()
						.Text(x =>
						{
							x.Span("Page ");
							x.CurrentPageNumber();
						});
				});
			}).ShowInCompanionAsync();


			//await Document.Create(container =>
			//{
			//	container.Page(page =>
			//	{
			//		page.Size(PageSizes.A4);
			//		page.Margin(20, Unit.Centimetre);
			//		page.PageColor(Colors.White);

			//		// Font configuration
			//		page.DefaultTextStyle(x => x.FontSize(12));

			//		// Header

			//		page.Header()
			//		.Text("Historial de armas cargadas")
			//		.FontFamily("Arial")
			//		.FontSize(16)
			//		.Bold()
			//		.AlignCenter();


			//		// Body
			//		//page.Content()
			//		//.PaddingVertical(10, Unit.Centimetre)
			//		//.PaddingHorizontal(10, Unit.Centimetre);

			//	});
			//}).ShowInCompanionAsync();




		}
	}
}
