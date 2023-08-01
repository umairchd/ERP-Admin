namespace ERP.Models.DropdownListModels
{
    public class VendorsDdl
    {
        public long VendorId { get; set; }
        public string Name { get; set; }
    }
    public class BankAccountsDdl
    {
        public long SupplierBankAccountId { get; set; }
        public string AccountTitle { get; set; }
    }
    public class PaymentMethodDdl
    {
        public int PaymentMethodId { get; set; }
        public string Title { get; set; }
    }
}
