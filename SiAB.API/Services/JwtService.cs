using Microsoft.IdentityModel.Tokens;
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
		private readonly string _secretKey;
		private const int EXPIRATION_TIME = 2;

		public JwtService(string secretKey)
		{
			_secretKey = secretKey;
		}

		public AuthenticatedResponse GenerateToken(Usuario usuario, string[] roles)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_secretKey);
			var expiration = DateTime.UtcNow.AddHours(EXPIRATION_TIME);	

			var tokenDescriptor = new SecurityTokenDescriptor
			{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
				new Claim(ClaimTypes.Name, usuario.UserName),
				new Claim(ClaimTypes.Role, string.Join(",", roles)),			
			}),
			Expires = expiration,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new AuthenticatedResponse 
			{ 
				Token = tokenHandler.WriteToken(token),
				Expiration = expiration
			}; 
		}
	}
}
