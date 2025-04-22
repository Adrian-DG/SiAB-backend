namespace SiAB.API.Helpers
{
	public class UserContextService : IUserContextService
	{
		public int CodUsuario { get; set; }
		public int CodInstitucionUsuario { get; set; }
		public string[] Roles { get; set; } = Array.Empty<string>();
	}
}
