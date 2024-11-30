using AutoMapper;
using SiAB.Core.DTO.Auth;
using SiAB.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAB.Core.Profiles.Auth
{
	public class UsuarioProfile : Profile
	{
		public UsuarioProfile()
		{
			CreateMap<UsuarioRegisterDto, Usuario>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
		}
	}
}
