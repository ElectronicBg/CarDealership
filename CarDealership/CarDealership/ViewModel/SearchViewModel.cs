using CarDealership.Models;
using CarDealership.Models.Enums;

namespace CarDealership.ViewModel
{
    public class SearchViewModel
    {
        public int? CarId { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public EngineType? EngineType { get; set; }
        public TransmissionType? TransmissionType { get; set; }
        public int? CarColorId { get; set; }
        public Region? Region { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? Mileage { get; set; }
        public int Power { get; set; }
        public CarType? CarType { get; set; }
        public Condition? Condition { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<Photo> Photos { get; set; }
        public string OrderBy { get; set; }
        public int PageSize {  get; set; }
        public string PriceRange => $"{MinPrice:C} - {MaxPrice:C}";
        public string YearRange => $"{MinYear:C} - {MaxYear:C}";
    }
}
