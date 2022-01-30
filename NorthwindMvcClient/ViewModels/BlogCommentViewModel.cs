using System.ComponentModel;

namespace NorthwindMvcClient.ViewModels
{
    public class BlogCommentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Publisher")]
        public int PublisherId { get; set; }

        [DisplayName("Publisher")]
        public string PublisherName { get; set; }
        public int ArticleId { get; set; }
        public string? Comment { get; set; }
    }
}
