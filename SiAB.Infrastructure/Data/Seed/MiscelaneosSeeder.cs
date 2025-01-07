using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Belico;
using SiAB.Core.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data.Seed
{
	public static class MiscelaneosSeeder
	{
		public static void Seed(ModelBuilder builder)
		{
			builder.Entity<Propiedad>().HasData(
				new Propiedad { Id = 1, Nombre = "ARMA DE SU PROPIEDAD" },
				new Propiedad { Id = 2, Nombre = "FF.AA" },
				new Propiedad { Id = 3, Nombre = "PERDIDA" }
			);

			builder.Entity<Categoria>().HasData(
				new Categoria { Id = 1, Nombre = "ARMAS" }
			);

			builder.Entity<Tipo>().HasData(
				new Tipo { Id = 1, Nombre = "ACCESORIO(S) DE ARMAS" },
				new Tipo { Id = 2, Nombre = "ARMAS BLANCAS" },
				new Tipo { Id = 3, Nombre = "ARMAS CORTAS" },
				new Tipo { Id = 4, Nombre = "ARMAS LARGAS" },
				new Tipo { Id = 5, Nombre = "ARMAS PESADAS" },
				new Tipo { Id = 6, Nombre = "EQUIPO DE COMUNICACION" },
				new Tipo { Id = 7, Nombre = "EQUIPO DE PROTECCION" },
				new Tipo { Id = 8, Nombre = "EQUIPO DE TRANSPORTE" },
				new Tipo { Id = 9, Nombre = "EQUIPO DE VIGILANCIA" },
				new Tipo { Id = 10, Nombre = "EXPLOSIVOS" },
				new Tipo { Id = 11, Nombre = "MUNICIONES" }
			);

			builder.Entity<SubTipo>().HasData(
				new SubTipo { Id = 1, Nombre = "AMETRALLADORAS", TipoId = 4 },
				new SubTipo { Id = 2, Nombre = "CARABINAS", TipoId = 4 },
				new SubTipo { Id = 3, Nombre = "CASCOS", TipoId = 7 },
				new SubTipo { Id = 4, Nombre = "CHALECOS", TipoId = 7 },
				new SubTipo { Id = 5, Nombre = "ESCOPETAS", TipoId = 4 },
				new SubTipo { Id = 6, Nombre = "FUSIL", TipoId = 4 },
				new SubTipo { Id = 7, Nombre = "GRANADAS", TipoId = 10 },
				new SubTipo { Id = 8, Nombre = "LANZACOHETES", TipoId = 5 },
				new SubTipo { Id = 9, Nombre = "MINAS", TipoId = 10 },
				new SubTipo { Id = 10, Nombre = "PISTOLAS", TipoId = 3 },
				new SubTipo { Id = 11, Nombre = "RADIOS", TipoId = 6 },
				new SubTipo { Id = 12, Nombre = "REVOLVERES", TipoId = 3 },
				new SubTipo { Id = 13, Nombre = "SUBAMETRALLADORAS", TipoId = 3 }
			);

			builder.Entity<Marca>().HasData(
				new Marca { Id = 1, Nombre = "BERETTA" },
				new Marca { Id = 2, Nombre = "BROWNING" },
				new Marca { Id = 3, Nombre = "CARANDAI" },
				new Marca { Id = 4, Nombre = "COLT" },
				new Marca { Id = 5, Nombre = "GLOCK" },
				new Marca { Id = 6, Nombre = "HECKLER & KOCH" },
				new Marca { Id = 7, Nombre = "MAVERICK" },
				new Marca { Id = 8, Nombre = "MOSSBERG" },
				new Marca { Id = 9, Nombre = "RAVEN" },
				new Marca { Id = 10, Nombre = "REMINGTON" },
				new Marca { Id = 11, Nombre = "RUGER" },
				new Marca { Id = 12, Nombre = "SAVAGE" },
				new Marca { Id = 13, Nombre = "SIG SAUER" },
				new Marca { Id = 14, Nombre = "SMITH & WESSON (S&W)" },
				new Marca { Id = 15, Nombre = "TAURUS" },
				new Marca { Id = 16, Nombre = "WINCHESTER" }
			);

			builder.Entity<Modelo>().HasData(
				new Modelo { Id = 1, Nombre = "92FS", MarcaId = 1 },
				new Modelo { Id = 2, Nombre = "PX4 Storm", MarcaId = 1 },
				new Modelo { Id = 3, Nombre = "Hi-Power", MarcaId = 2 },
				new Modelo { Id = 4, Nombre = "BAR", MarcaId = 2 },
				new Modelo { Id = 5, Nombre = "Carandai 1911", MarcaId = 3 },
				new Modelo { Id = 6, Nombre = "Carandai 380", MarcaId = 3 },
				new Modelo { Id = 7, Nombre = "M1911", MarcaId = 4 },
				new Modelo { Id = 8, Nombre = "Python", MarcaId = 4 },
				new Modelo { Id = 9, Nombre = "G17", MarcaId = 5 },
				new Modelo { Id = 10, Nombre = "G19", MarcaId = 5 }
			);

			builder.Entity<Calibre>().HasData(
					new Calibre { Id = 1, Nombre = "12mm" },
					new Calibre { Id = 2, Nombre = "9mm" },
					new Calibre { Id = 3, Nombre = ".22mm" },
					new Calibre { Id = 4, Nombre = "5.56mm" }
				);


		}
	}
}
