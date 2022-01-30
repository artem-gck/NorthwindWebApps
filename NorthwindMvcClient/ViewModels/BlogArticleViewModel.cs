using System.ComponentModel;

namespace NorthwindMvcClient.ViewModels
{
    public class BlogArticleViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the date published.
        /// </summary>
        /// <value>
        /// The date published.
        /// </value>
        [DisplayName("Date Published")]
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<BlogCommentViewModel> Comments { get; set; }

        public int AddingProductId { get; set; }
    }
}
