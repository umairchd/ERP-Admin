﻿using System;

namespace ERP.Models.ReportsModels
{
    public class PurchaseReport
    {
        public long? VendorId { get; set; }
        public long ProductCode { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public long PurchasedQuantity { get; set; }
        public DateTime PurchasingDate { get; set; }
    }
}
