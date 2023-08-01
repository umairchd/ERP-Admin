using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Service.Services
{
    public class InventoryItemService:IInventoryItemService
    {
        private readonly IUnitRepository unitRepository;
        private readonly IShelfRepository shelfRepository;
        private readonly IPurchaseBillItemRepositoy inventoryItemRepositoy;
        private readonly IProductVariationRepository productVariationRepository;
        private readonly IProductRepository productRepository;
        private readonly IVendorRepository vendorRepository;

        public InventoryItemService(IUnitRepository unitRepository,IShelfRepository shelfRepository,IPurchaseBillItemRepositoy inventoryItemRepositoy,IProductVariationRepository productVariationRepository,IProductRepository productRepository, IVendorRepository vendorRepository)
        {
            this.unitRepository = unitRepository;
            this.shelfRepository = shelfRepository;
            this.inventoryItemRepositoy = inventoryItemRepositoy;
            this.productVariationRepository = productVariationRepository;
            this.productRepository = productRepository;
            this.vendorRepository = vendorRepository;
        }

        public PurchaseBillItem GetInventoryItem(long inventoryItemId)
        {
            return inventoryItemRepositoy.Find(inventoryItemId);
        }

        public IEnumerable<PurchaseBillItem> GetAllInventoryItems()
        {
            return inventoryItemRepositoy.GetAll();
        }

        public long AddInventoryItem(PurchaseBillItem inventoryItem)
        {
            if (inventoryItem.ItemId>0)
                inventoryItemRepositoy.Update(inventoryItem);
            else
                inventoryItemRepositoy.Add(inventoryItem);
            inventoryItemRepositoy.SaveChanges();

            //Update Product Price etc
            UpdateProduct(inventoryItem);

            return inventoryItem.ItemId;
        }

        public InventoryItemSearchResponse GetInventoryItemSearchResponse(InventoryItemSearchRequest searchRequest)
        {
            return inventoryItemRepositoy.GetInventoryItemSearchResponse(searchRequest);
        }

        public InventoryItemResponse GetInventoryItemResponse(long? inventoryItemId)
        {
            InventoryItemResponse inventoryItemResponse=new InventoryItemResponse();
            if (inventoryItemId != null)
            {
                var inventoryItem = GetInventoryItem((long)inventoryItemId);
                if (inventoryItem != null)
                    inventoryItemResponse.InventoryItem = inventoryItem;
            }
            inventoryItemResponse.Vendors = vendorRepository.GetActiveVendors().ToList();
            inventoryItemResponse.Units = unitRepository.GetAll().ToList();
            inventoryItemResponse.Shelf = shelfRepository.GetAll().ToList();

            return inventoryItemResponse;
        }

        private void UpdateProduct(PurchaseBillItem inventoryItem)
        {
            var product = productVariationRepository.Find(inventoryItem.ProductVariationId);
            if (product == null || (product.Purchaseprice == inventoryItem.PurchasePrice && product.Saleprice == inventoryItem.SalePrice))
                return;

            product.Purchaseprice = inventoryItem.PurchasePrice;
            product.Saleprice = inventoryItem.SalePrice;

            productVariationRepository.Update(product);
            productVariationRepository.SaveChanges();
        }
    }
}
