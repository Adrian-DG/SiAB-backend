using SiAB.Application.Contracts;
using SiAB.Core.Entities.Sipffaa;
using SiAB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Previewer;
using QuestPDF.Companion;
using SiAB.Core.DTO.Transacciones;

namespace SiAB.Infrastructure.Repositories
{
	public class ReportRepository : IReportRepository
	{
		private readonly AppDbContext _context;
		public ReportRepository(AppDbContext appDbContext)
		{
			_context = appDbContext;
			QuestPDF.Settings.License = LicenseType.Community;
		}

		public async Task<byte[]> GetHistorialArmasCargadas(ConsultaMiembro miembro)
		{
			var document = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(20);
					page.PageColor(Colors.White);

					// Font configuration
					page.DefaultTextStyle(x => x.FontSize(12));

					// Header

					page.Header()
					.Text("Historial de armas cargadas")
					.Bold()
					.FontSize(20)
					.AlignCenter();

					// Body
					page.Content()
					.PaddingVertical(10, Unit.Centimetre)
					.PaddingHorizontal(10, Unit.Centimetre);

				});
			});

			document.GeneratePdf();

			await document.ShowInCompanionAsync();

			return document.GeneratePdf();
		}
	}
}
