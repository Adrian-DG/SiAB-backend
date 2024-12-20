﻿using Microsoft.IdentityModel.Tokens;
using SiAB.API.Constants;
using SiAB.Core.Entities.Auth;
using SiAB.Core.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiAB.API.Services
{
	public class JwtService
	{
		private const int EXPIRATION_MINUTES = 30;
		private readonly IConfiguration _configuration;

		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public AuthenticatedResponse CreateToken(Usuario user, IList<string> roles)
		{
			var expiration = DateTime.Now.AddMinutes(EXPIRATION_MINUTES);

			var token = CreateJwtToken(CreateClaims(user, roles), CreateCredentials(), expiration);

			var tokenHandler = new JwtSecurityTokenHandler();

			return new AuthenticatedResponse
			{
				Token = tokenHandler.WriteToken(token),
				Expiration = expiration
			};
		}

		private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration)
		{
			return new JwtSecurityToken(
				_configuration[JwtBearerConstants.JWT_ISSUER],
				_configuration[JwtBearerConstants.Jwt_Audience],
				claims,
				expires: expiration,
				signingCredentials: credentials
			
			);
		}

		private Claim[] CreateClaims(Usuario user, IList<string> roles)
		{
			return new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, _configuration[JwtBearerConstants.Jwt_Subject]),
				new Claim(JwtRegisteredClaimNames.Aud, _configuration[JwtBearerConstants.Jwt_Audience]),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
				new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Name, user.UserName),
				new Claim(ClaimTypes.Role, string.Join(",", roles)),
				// new Claim("CodInstitucion", user.Institucion.ToString()),
            };
		}

		private SigningCredentials CreateCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_configuration[JwtBearerConstants.JWT_KEY]);
			return new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature
			);
		}
	}
}
