using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.BasketModuleDtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}