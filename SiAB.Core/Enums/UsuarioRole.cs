using System.ComponentModel;

namespace SiAB.Core.Enums
{
    public enum UsuarioRole
    {
        [Description("Administrador, puede realizar cualquier acción")]
        ADMIN = 1,
        [Description("Usuario, puede realizar acciones básicas")]
        USER
    }
}