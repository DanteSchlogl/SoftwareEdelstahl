using Edelstahl.BLL.Exceptions.Base;

namespace Edelstahl.BLL.Exceptions.Commercial
{
    /// <summary>
    /// Se produce cuando el CUIT ingresado no cumple
    /// con las reglas establecidas.
    /// </summary>
    public sealed class CUITInvalidoException
        : BusinessRuleException
    {
        public string CUIT { get; }

        public CUITInvalidoException(string cuit)
            : base(
                "El CUIT ingresado no es válido. Debe contener 11 dígitos.",
                "CLIENTE_CUIT_INVALIDO")
        {
            CUIT = cuit;
        }
    }
}