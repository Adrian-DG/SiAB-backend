using AutoMapper;
using SiAB.Core.DTO.Misc;
using SiAB.Core.Entities.Inventario;
using SiAB.Core.Entities.Misc;
using SiAB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Profiles.Miscelaneos
{
	public class MiscelaneosProfile : Profile
	{
		public MiscelaneosProfile() 
		{
			#region Depositos
			CreateMap<Deposito, NamedModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
			#endregion

			#region Categorias
			CreateMap<Categoria, NamedModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

			CreateMap<List<Categoria>, List<NamedModel>>();				

			#endregion

			#region Tipos
			CreateMap<Tipo, NamedModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
			#endregion
		}
	}
}
