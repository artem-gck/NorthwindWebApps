using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Blogging
{
    /// <summary>
    /// IBloggingService interface.
    /// </summary>
    public interface IBloggingService
    {
        /// <summary>
        /// Creates the article asynchronous.
        /// </summary>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        Task<int> CreateArticleAsync(BlogArticle article);

        /// <summary>
        /// Shows the articles asynchronous.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit);

        /// <summary>
        /// Tries the show article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        bool TryShowArticle(int articleId, out BlogArticle article);

        /// <summary>
        /// Destroys the article asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns></returns>
        Task<bool> DestroyArticleAsync(int articleId);

        /// <summary>
        /// Updates the article asynchronous.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="article">The article.</param>
        /// <returns></returns>
        Task<bool> UpdateArticleAsync(int articleId, BlogArticle article);
    }
}
