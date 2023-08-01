﻿using System;
using System.Collections.Generic;
using ERP.Models.ReportsModels;

namespace ERP.Interfaces.IServices
{
    public interface IReportsService
    {
        IEnumerable<SalesReport> SalesReport(string productCode,DateTime startDate, DateTime endDate);
        IEnumerable<PurchaseReport> PurchaseReport(string productCode, long vendorId, DateTime startDate, DateTime endDate);
        IEnumerable<StockReport> StocksReport(string barCode, string productCode, string productName);
        IEnumerable<ExpenseReport> ExpensesReport(int year, int month, string vendor);
    }
}
