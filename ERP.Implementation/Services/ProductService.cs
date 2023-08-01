using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Service.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductVariationRepository productVariationRepository;
        private readonly IBrandService brandService;
        private readonly IProductRepository productRepository;
        private readonly IPurchaseBillItemRepositoy inventoryItemRepositoy;
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IUnitRepository unitRepository;
        private readonly IShelfRepository shelfRepository;

        public ProductService(IProductVariationRepository productVariationRepository,IBrandService brandService, IProductRepository productRepository, IPurchaseBillItemRepositoy inventoryItemRepositoy, IOrderItemsRepository orderItemsRepository, IProductCategoryRepository productCategoryRepository, IUnitRepository unitRepository, IShelfRepository shelfRepository)
        {
            this.productVariationRepository = productVariationRepository;
            this.brandService = brandService;
            this.productRepository = productRepository;
            this.inventoryItemRepositoy = inventoryItemRepositoy;
            this.orderItemsRepository = orderItemsRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.unitRepository = unitRepository;
            this.shelfRepository = shelfRepository;
        }

        public Product GetProduct(long productId)
        {
            return productRepository.Find(productId);
        }

        public ProductVariation GetProductVariation(long productVariationId)
        {
            return productVariationRepository.Find(productVariationId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productRepository.GetAll().ToList();
        }

        public IEnumerable<ProductVariation> GetAllProductVariations()
        {
            return productVariationRepository.GetAll().ToList();
        }

        public long SaveProduct(Product product)
        {
            if (product.ProductId > 0)
            {
                productRepository.Update(product);
                foreach (var productVariation in product.ProductVariations)
                {
                    productVariationRepository.Update(productVariation);
                }
                productVariationRepository.SaveChanges();
            }
            else
                productRepository.Add(product);
            
            productRepository.SaveChanges();
            return product.ProductId;
        }

        public ProductSearchResponse GetProductSearchResponse(ProductSearchRequest searchRequest)
        {
            return productRepository.GetProductSearchResponse(searchRequest);
        }

        public ProductVariationSearchResponse GetProductVariationSearchResponse(ProductVariationSearchRequest searchRequest)
        {
            return productVariationRepository.GetProductVariationSearchResponse(searchRequest);
        }

        public WebsiteProductSearchResponse GetWebsiteProductSearchResponse(WebsiteProductSearchRequest searchRequest)
        {
            return productRepository.GetWebsiteProductSearchResponse(searchRequest);
        }

        public ProductResponse GetProductResponse(long? productId)
        {
            ProductResponse responseResult = new ProductResponse();
            if (productId != null)
            {
                var product = GetProduct((long)productId);
                if (product != null)
                    responseResult.Product = product;
            }
            responseResult.ProductBrands = brandService.GetAllBrands().ToList();//ADDED BY UMER
            responseResult.ProductCategories = productCategoryRepository.GetAllSubCategories().ToList();
            responseResult.Units = unitRepository.GetAll().ToList();
            responseResult.Shelves = shelfRepository.GetAll().ToList();
            return responseResult;
        }

        public ProductSearchResponseByAnyCode GetProductByAnyCode(string code)
        {
            var response=new ProductSearchResponseByAnyCode();
            var productVariation = productVariationRepository.GetProductByAnyCode(code);
            if (productVariation != null)
            {
                response.Product = productVariation;
                response.AvailableItems = GetAvailableProductItem(productVariation.ProductVariationId);
            }
            return response;
        }
        public long GetAvailableProductItem(long productVariationId)
        {
            var itemInInventory = inventoryItemRepositoy.GetItemCountInInventory(productVariationId);
            var itemInOrders = orderItemsRepository.GetItemCountInOrders(productVariationId);
            return itemInInventory - itemInOrders;

        }
        //Added by Umer--<
        public IEnumerable<Product> GetProductsById(long id)
        {
            return productRepository.GetProductsById(id);
        }

        public IEnumerable<Product> GetProductsByCategories(long mainCategoryId, long subCategoryId)
        {
            return productRepository.GetProductsByCategories(mainCategoryId, subCategoryId);
        }
        public IEnumerable<ProductVariation> GetProductVariationsByCategories(long mainCategoryId, long subCategoryId)
        {
            return productVariationRepository.GetProductVariationsByCategories(mainCategoryId, subCategoryId);
        }
        public IEnumerable<Product> GetLatestProducts()
        {
             return productRepository.GetLatestProducts();
        }

        public IEnumerable<Product> GetFeaturedProducts()
        {
            return productRepository.GetFeaturedProducts();
        }

        public bool DeleteProductVariation(long id)
        {
            var item = productVariationRepository.Find(id);
            if (item.OrderItems.Any())
            {
                return false;
            }
            else
            {
                productVariationRepository.Delete(item);
                productVariationRepository.SaveChanges();
                return true;
            }
        }

        //-->

    }
}
