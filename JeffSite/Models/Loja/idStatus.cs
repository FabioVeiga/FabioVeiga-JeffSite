using System.ComponentModel;

namespace JeffSite.Models{
    public enum Status
    {
        [Description("Aguardando Link")]
        Aguardando_Link = 1,

        [Description("Aguardando Pagamento")]
        Aguardando_Pagamento = 2,

        [Description("Pago e Aguardando dedicatório")]
        Pago_e_Aguardando_Dedicatorio = 3,
        Enviado = 4
    }
}