namespace NorthwindMvcClient.ViewModels
{
    public class BlogArticlesListViewModel
    {
        public IEnumerable<BlogArticleViewModel> BlogArticles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
