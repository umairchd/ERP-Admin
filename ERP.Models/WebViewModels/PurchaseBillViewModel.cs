using System.Collections.Generic;
using ERP.Models.DropdownListModels;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;
using PurchaseBill = ERP.Models.WebModels.PurchaseBill;

namespace ERP.Models.WebViewModels
{
    public class PurchaseBillViewModel
    {
        public PurchaseBillViewModel()
        {
            PurchaseBill=new PurchaseBill();
            SupplierPaymentHistory = new SupplierPaymentHistoryModel();
            InventoryItems = new List<InventoryItemModel>();
            Vendors = new List<VendorsDdl>();
            SupplierAccountsDdl = new List<BankAccountsDdl>();
            PaymentMethods= new List<PaymentMethodDdl>();
        }
        public PurchaseBill PurchaseBill { get; set; }
        public SupplierPaymentHistoryModel SupplierPaymentHistory { get; set; }
        public List<InventoryItemModel> InventoryItems { get; set; }
        public List<VendorsDdl> Vendors { get; set; }
        public List<PaymentMethodDdl> PaymentMethods { get; set; }
        public List<BankAccountsDdl> SupplierAccountsDdl { get; set; }

        public decimal SubTotal { get; set; }
        public decimal GrossTotal { get; set; }
    }

    public class PurchaseBillHistoryViewModel
    {
        public PurchaseBillHistoryViewModel()
        {
            data = new List<PurchaseBillHistory>();
            VendorsDdl = new List<VendorsDdl>();
            SearchRequest = new PuchaseBillsSearchRequest();
        }
        public List<PurchaseBillHistory> data { get; set; }
        public List<VendorsDdl> VendorsDdl { get; set; }
        public PuchaseBillsSearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        
        public int recordsFiltered;
    }
}
