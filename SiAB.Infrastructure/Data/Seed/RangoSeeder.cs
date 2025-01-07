using Microsoft.EntityFrameworkCore;
using SiAB.Core.Entities.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Infrastructure.Data.Seed
{
	public static class RangoSeeder
	{
		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Rango>().HasData(
					new Rango { Id = 1,  Nombre = "MAYOR GENERAL", NombreArmada = "VICEALMIRANTE" },
					new Rango { Id = 2,  Nombre = "GENERAL", NombreArmada = "CONTRALMIRANTE" },
					new Rango { Id = 3,  Nombre = "CORONEL", NombreArmada = "CAPITAN DE NAVIO" },
					new Rango { Id = 4,  Nombre = "TENIENTE CORONEL", NombreArmada = "CAPITAN DE FRAGATA" },
					new Rango { Id = 5,  Nombre = "MAYOR", NombreArmada = "CAPITAN CORBETA" },
					new Rango { Id = 6,  Nombre = "CAPITAN", NombreArmada = "TENIENTE NAVIO" },
					new Rango { Id = 7,  Nombre = "1ER TENIENTE", NombreArmada = "TENIENTE FRAGATA" },
					new Rango { Id = 8,  Nombre = "2DO TENIENTE/TENIENTE CORBETA", NombreArmada = "TENIENTE CORBETA" },
					new Rango { Id = 9,  Nombre = "SARGENTO MAYOR", NombreArmada = "SARGENTO MAYOR" },
					new Rango { Id = 10, Nombre = "SARGENTO", NombreArmada = "SARGENTO" },
					new Rango { Id = 11, Nombre = "CABO", NombreArmada = "CABO" },
					new Rango { Id = 12, Nombre = "RASO/MARINERO", NombreArmada = "MARINERO" },
					new Rango { Id = 13, Nombre = "ASIMILADO", NombreArmada = "ASIMILADO" }
			);
		}
	}
}
