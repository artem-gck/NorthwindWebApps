namespace NorthwindMvcClient.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
