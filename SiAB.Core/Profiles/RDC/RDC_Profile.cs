using AutoMapper;
using SiAB.Core.DTO.CargoDescargo;
using SiAB.Core.Entities.Belico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Profiles.CargoDescargo
{
	public class RDC_Profile : Profile
	{
		public RDC_Profile()
		{
			CreateMap<CreateRDC_Dto, RegistroDebitoCredito>()
				.ForMember(dest => dest.DebitoA, opt => opt.MapFrom(src => src.DebitoA))
				.ForMember(dest => dest.TipoDebito, opt => opt.MapFrom(src => src.TipoDebito))
				.ForMember(dest => dest.CreditoA, opt => opt.MapFrom(src => src.CreditoA))
				.ForMember(dest => dest.TipoCredito, opt => opt.MapFrom(src => src.TipoCredito))
				.ForMember(dest => dest.Articulos, opt => opt.MapFrom(src => src.Articulos));				
		}
	}
}
