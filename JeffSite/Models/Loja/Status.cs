using System.ComponentModel;
namespace JeffSite.Models.Loja{
    public enum Status
    {
        [Description("Aguardando Link de Pagamento")]
        Aguardando_Link_De_Pagamento = 1,

        [Description("Aguardando Pagamento")]
        Aguardando_Pagamento = 2,

        [Description("Pago e Aguardando dedicatório")]
        Pago_e_Aguardando_Dedicatorio = 3,
        Enviado = 4
    }
}