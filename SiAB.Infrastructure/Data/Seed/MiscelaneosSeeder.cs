using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
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
			builder.Entity<Dependencia>().HasData(
				new Dependencia { Id = 1, Nombre = "MINISTERIO DE DEFENSA (MIDE)", Institucion = Core.Enums.InstitucionEnum.MIDE },
				new Dependencia { Id = 2, Nombre = "EJÉRCITO DE LA REPÚBLICA DOMINICANA (ERD)", Institucion = Core.Enums.InstitucionEnum.MIDE },
				new Dependencia { Id = 3, Nombre = "ARMADA DE LA REPÚBLICA DOMINICANA (ARD)", Institucion = Core.Enums.InstitucionEnum.MIDE },
				new Dependencia { Id = 4, Nombre = "FUERZA AEREA DE LA REPÚBLICA DOMINICANA (FARD)", Institucion = Core.Enums.InstitucionEnum.MIDE },
				new Dependencia { Id = 5, Nombre = "POLICIA NACIONAL (PN)", Institucion = Core.Enums.InstitucionEnum.MIDE }
			);

			builder.Entity<Categoria>().HasData(
				new Categoria { Id = 1, Nombre = "NO DEFINIDA" },
				new Categoria { Id = 2, Nombre = "ARMAS" },
				new Categoria { Id = 3, Nombre = "EQUIPO" },
				new Categoria { Id = 4, Nombre = "MUNICIONES" },
				new Categoria { Id = 5, Nombre = "EXPLOSIVOS" },
				new Categoria { Id = 6, Nombre = "ACCESORIOS" }
			);

			builder.Entity<Tipo>().HasData(
				new Tipo { Id = 1, Nombre = "NO DEFINIDO", CategoriaId = 1 },
				new Tipo { Id = 2, Nombre = "ACCESORIO(S) DE ARMAS", CategoriaId = 5 },
				new Tipo { Id = 3, Nombre = "ARMAS BLANCAS", CategoriaId = 1 },
				new Tipo { Id = 4, Nombre = "ARMAS CORTAS", CategoriaId = 1 },
				new Tipo { Id = 5, Nombre = "ARMAS LARGAS", CategoriaId = 1 },
				new Tipo { Id = 6, Nombre = "ARMAS PESADAS", CategoriaId = 1 },
				new Tipo { Id = 7, Nombre = "EQUIPO DE COMUNICACION", CategoriaId = 2 },
				new Tipo { Id = 8, Nombre = "EQUIPO DE PROTECCION", CategoriaId = 2 },
				new Tipo { Id = 9, Nombre = "EQUIPO DE TRANSPORTE", CategoriaId = 2 },
				new Tipo { Id = 10, Nombre = "EQUIPO DE VIGILANCIA", CategoriaId = 2 },
				new Tipo { Id = 11, Nombre = "EXPLOSIVOS", CategoriaId = 5 },
				new Tipo { Id = 12, Nombre = "MUNICIONES", CategoriaId = 3 }
			);

			builder.Entity<SubTipo>().HasData(
				new SubTipo { Id = 1, Nombre = "NO DEFINIDO", TipoId = 1 },
				new SubTipo { Id = 2, Nombre = "AMETRALLADORAS", TipoId = 4 },
				new SubTipo { Id = 3, Nombre = "CARABINAS", TipoId = 4 },
				new SubTipo { Id = 4, Nombre = "CASCOS", TipoId = 7 },
				new SubTipo { Id = 5, Nombre = "CHALECOS", TipoId = 7 },
				new SubTipo { Id = 6, Nombre = "ESCOPETAS", TipoId = 4 },
				new SubTipo { Id = 7, Nombre = "FUSIL", TipoId = 4 },
				new SubTipo { Id = 8, Nombre = "GRANADAS", TipoId = 10 },
				new SubTipo { Id = 9, Nombre = "LANZACOHETES", TipoId = 5 },
				new SubTipo { Id = 10, Nombre = "MINAS", TipoId = 11 },
				new SubTipo { Id = 11, Nombre = "PISTOLAS", TipoId = 3 },
				new SubTipo { Id = 12, Nombre = "RADIOS", TipoId = 6 },
				new SubTipo { Id = 13, Nombre = "REVOLVERES", TipoId = 3 },
				new SubTipo { Id = 14, Nombre = "SUBAMETRALLADORAS", TipoId = 3 }
			);

			builder.Entity<Marca>().HasData(
				new Marca { Id = 1, Nombre = "NO DEFINIDA" },
				new Marca { Id = 2, Nombre = "BERETTA" },
				new Marca { Id = 3, Nombre = "BROWNING" },
				new Marca { Id = 4, Nombre = "CARANDAI" },
				new Marca { Id = 5, Nombre = "COLT" },
				new Marca { Id = 6, Nombre = "GLOCK" },
				new Marca { Id = 7, Nombre = "HECKLER & KOCH (H&K)" },
				new Marca { Id = 8, Nombre = "MAVERICK" },
				new Marca { Id = 9, Nombre = "MOSSBERG" },
				new Marca { Id = 10, Nombre = "RAVEN" },
				new Marca { Id = 11, Nombre = "REMINGTON" },
				new Marca { Id = 12, Nombre = "RUGER" },
				new Marca { Id = 13, Nombre = "SAVAGE" },
				new Marca { Id = 14, Nombre = "SIG SAUER" },
				new Marca { Id = 15, Nombre = "SMITH & WESSON (S&W)" },
				new Marca { Id = 16, Nombre = "TAURUS" },
				new Marca { Id = 17, Nombre = "WINCHESTER" }				
			);

			builder.Entity<Modelo>().HasData(
				new Modelo { Id = 1, Nombre = "NO DEFINIDO", MarcaId = 1 }
			);

			builder.Entity<Calibre>().HasData(
					new Calibre { Id = 1, Nombre = "NO DEFINIDO" },
					new Calibre { Id = 2, Nombre = "12mm" },
					new Calibre { Id = 3, Nombre = "9mm" },
					new Calibre { Id = 4, Nombre = ".22mm" },
					new Calibre { Id = 5, Nombre = "5.56mm" }
				);

			builder.Entity<TipoDocumento>().HasData(
					new TipoDocumento { Id = 1, Nombre = "NO DEFINIDO" },
					new TipoDocumento { Id = 2, Nombre = "FORMULARIO #53" },
					new TipoDocumento { Id = 3, Nombre = "LICENCIA" },
					new TipoDocumento { Id = 4, Nombre = "AUTORIZACIÓN IMPORTACIÓN" },
					new TipoDocumento { Id = 5, Nombre = "AUTORIZACIÓN RETIRO ADUANAL" }
				);


		}
	}
}
