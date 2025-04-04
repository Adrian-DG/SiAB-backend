using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Entities.Personal;
using SiAB.Core.Enums;
using SiAB.Core.Extensions;
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
				new Categoria { Id = (int)CategoriaArticuloEnum.NO_DEFINIDA, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.NO_DEFINIDA) },
				new Categoria { Id = (int)CategoriaArticuloEnum.ARMAS, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.ARMAS) },
				new Categoria { Id = (int)CategoriaArticuloEnum.MUNICIONES, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.MUNICIONES) },				
				new Categoria { Id = (int)CategoriaArticuloEnum.EXPLOSIVOS, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.EXPLOSIVOS) },
				new Categoria { Id = (int)CategoriaArticuloEnum.EQUIPOS, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.EQUIPOS) },
				new Categoria { Id = (int)CategoriaArticuloEnum.QUIMICOS, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.QUIMICOS) },
				new Categoria { Id = (int)CategoriaArticuloEnum.ACCESORIOS, Nombre = CategoriaArmaEnumExtensions.GetDescription(CategoriaArticuloEnum.ACCESORIOS) }
			);

			builder.Entity<Tipo>().HasData(
				new Tipo { Id = (int)TipoArticuloEnum.NO_DEFINIDO, Nombre = "NO DEFINIDO", CategoriaId = (int)CategoriaArticuloEnum.NO_DEFINIDA },
				new Tipo { Id = (int)TipoArticuloEnum.ACCESORIOS_DE_ARMAS, Nombre = "ACCESORIO(S) DE ARMAS", CategoriaId = (int)CategoriaArticuloEnum.ACCESORIOS },
				new Tipo { Id = (int)TipoArticuloEnum.ARMAS_BLANCAS, Nombre = "ARMAS BLANCAS", CategoriaId = (int)CategoriaArticuloEnum.ARMAS },
				new Tipo { Id = (int)TipoArticuloEnum.ARMAS_CORTAS, Nombre = "ARMAS CORTAS", CategoriaId = (int)CategoriaArticuloEnum.ARMAS },
				new Tipo { Id = (int)TipoArticuloEnum.ARMAS_LARGAS, Nombre = "ARMAS LARGAS", CategoriaId = (int)CategoriaArticuloEnum.ARMAS },
				new Tipo { Id = (int)TipoArticuloEnum.ARMAS_PESADAS, Nombre = "ARMAS PESADAS", CategoriaId = (int)CategoriaArticuloEnum.ARMAS },
				new Tipo { Id = (int)TipoArticuloEnum.EQUIPO_DE_COMUNICACION, Nombre = "EQUIPO DE COMUNICACION", CategoriaId = (int)CategoriaArticuloEnum.EQUIPOS },
				new Tipo { Id = (int)TipoArticuloEnum.EQUIPO_DE_PROTECCION, Nombre = "EQUIPO DE PROTECCION", CategoriaId = (int)CategoriaArticuloEnum.EQUIPOS },
				new Tipo { Id = (int)TipoArticuloEnum.EQUIPO_DE_TRANSPORTE, Nombre = "EQUIPO DE TRANSPORTE", CategoriaId = (int)CategoriaArticuloEnum.EQUIPOS },
				new Tipo { Id = (int)TipoArticuloEnum.EQUIPO_DE_VIGILANCIA, Nombre = "EQUIPO DE VIGILANCIA", CategoriaId = (int)CategoriaArticuloEnum.EQUIPOS },
				new Tipo { Id = (int)TipoArticuloEnum.EXPLOSIVOS, Nombre = "EXPLOSIVOS", CategoriaId = (int)CategoriaArticuloEnum.EXPLOSIVOS },
				new Tipo { Id = (int)TipoArticuloEnum.MUNICIONES, Nombre = "MUNICIONES", CategoriaId = (int)CategoriaArticuloEnum.MUNICIONES },
				new Tipo { Id = (int)TipoArticuloEnum.QUIMICOS, Nombre = "QUIMICOS", CategoriaId = (int)CategoriaArticuloEnum.QUIMICOS }
			);

			builder.Entity<SubTipo>().HasData(
				new SubTipo { Id = 1, Nombre = "NO DEFINIDO", TipoId = (int)TipoArticuloEnum.NO_DEFINIDO },
				new SubTipo { Id = 2, Nombre = "GRANADAS", TipoId = (int)TipoArticuloEnum.EXPLOSIVOS },
				new SubTipo { Id = 3, Nombre = "MINAS", TipoId = (int)TipoArticuloEnum.EXPLOSIVOS },
				new SubTipo { Id = 4, Nombre = "PISTOLAS", TipoId = (int)TipoArticuloEnum.ARMAS_CORTAS },
				new SubTipo { Id = 5, Nombre = "RADIOS", TipoId = (int)TipoArticuloEnum.EQUIPO_DE_COMUNICACION },
				new SubTipo { Id = 6, Nombre = "REVOLVERES", TipoId = (int)TipoArticuloEnum.ARMAS_CORTAS }
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
				new Calibre { Id = 1, Nombre = "NO DEFINIDO" }
			);

			builder.Entity<TipoDocumento>().HasData(
				new TipoDocumento { Id = 1, Nombre = "NO DEFINIDO" },
				new TipoDocumento { Id = 2, Nombre = "OFICIO" },
				new TipoDocumento { Id = 3, Nombre = "FORMULARIO #53" },
				new TipoDocumento { Id = 4, Nombre = "LICENCIA" },
				new TipoDocumento { Id = 5, Nombre = "AUTORIZACIÓN IMPORTACIÓN" },
				new TipoDocumento { Id = 6, Nombre = "AUTORIZACIÓN RETIRO ADUANAL" }
			);


		}
	}
}
