namespace ERP.Models.DomainModels
{
    
        public class ProductImage
        {
            public long ImageId { get; set; }
            public long ProductVariationId { get; set; }
            public byte[] ImageData { get; set; }
            public string ContentType { get; set; }
            public bool IsDefault { get; set; }
            public System.DateTime RecCreatedDate { get; set; }
            public System.DateTime RecLastUpdatedDate { get; set; }
            public string RecCreatedBy { get; set; }
            public string RecLastUpdatedBy { get; set; }

            public virtual ProductVariation ProductVariation { get; set; }
        }
    
}
