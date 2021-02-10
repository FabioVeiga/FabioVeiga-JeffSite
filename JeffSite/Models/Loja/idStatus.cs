using System;

namespace JeffSite.Models{
    public enum Status
    {
        Aguardando_Link = 1,
        Aguardando_Pagamento = 2,
        Pago_e_Aguardando_Dedicatorio = 3,
        Enviado = 4
    }
}