namespace DigiShop.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set;} = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set;} = new PagingInfo();

        public IEnumerable<Category>? Categories { get; set;} 
    }
}
