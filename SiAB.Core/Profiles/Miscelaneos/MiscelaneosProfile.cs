using AutoMapper;
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
			CreateMap<Deposito, NamedModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
		}
	}
}
