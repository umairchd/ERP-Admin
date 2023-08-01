using ERP.Models.DomainModels;
using ERP.Models.DropdownListModels;

namespace ERP.Models.ModelMapers
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethodDdl CreateFromServerToClient(this PaymentMethod source)
        {
            return new PaymentMethodDdl
            {
                Title = source.Title,
                PaymentMethodId = source.PaymentMethodId
            };
        }
    }
}