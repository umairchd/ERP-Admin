using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers.ReportsMappers;
using ERP.Models.ReportsModels;

namespace ERP.Service.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly IPurchaseBillItemRepositoy inventoryItemRepositoy;
        private readonly IProductsStockRepository productsStockRepository;
        private readonly IExpenseRepository expenseRepository;

        public ReportsService(IOrderItemsRepository orderItemsRepository, IPurchaseBillItemRepositoy inventoryItemRepositoy, IProductsStockRepository productsStockRepository, IExpenseRepository expenseRepository)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.inventoryItemRepositoy = inventoryItemRepositoy;
            this.productsStockRepository = productsStockRepository;
            this.expenseRepository = expenseRepository;
        }

        public IEnumerable<SalesReport> SalesReport(string productCode,DateTime startDate, DateTime endDate)
        {
            var report = orderItemsRepository.GetOrderItemsReport(productCode, startDate, endDate).ToList().Select(x => x.CreateReport()).OrderByDescending(x => x.Date);
            return report;
        }

        public IEnumerable<PurchaseReport> PurchaseReport(string productCode, long vendorId, DateTime startDate, DateTime endDate)
        {
            var report = inventoryItemRepositoy.PurchaseReport(productCode, vendorId, startDate, endDate).ToList().Select(x => x.CreateReport()).OrderBy(x=>x.PurchasingDate);
            return report;
        }

        public IEnumerable<StockReport> StocksReport(string barCode, string productCode, string productName)
        {
            var report = productsStockRepository.StocksReport(barCode, productCode, productName).Select(x => x.CreateReport()).OrderByDescending(x => x.AvailableQuantity);
            return report;
        }
        public IEnumerable<ExpenseReport> ExpensesReport(int year, int month, string vendor)
        {
            var report = expenseRepository.GetExpenses(year, month,vendor).Select(x => x.CreateReport());
            //var report = expenseRepository.GetAll().ToList().Select(x => x.CreateReport());
            return report;
        }
    }
}
