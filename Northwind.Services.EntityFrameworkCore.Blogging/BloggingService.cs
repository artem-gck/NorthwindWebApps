using Northwind.Services.Blogging;
using Northwind.Services.EntityFrameworkCore.Blogging.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.EntityFrameworkCore.Blogging
{
    /// <summary>
    /// BloggingService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.Blogging.IBloggingService" />
    public class BloggingService : IBloggingService
    {
        private static BloggingContext? _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloggingService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BloggingService(string[] args)
            => _context = new DesignTimeBloggingContextFactory().CreateDbContext(args);

        /// <inheritdoc/>
        public async Task<int> CreateArticleAsync(BlogArticle article)
        {
            _ = article is null ? throw new ArgumentNullException(nameof(article)) : article;

            var a = await _context.BlogArticles.AddAsync(GetArticleEnt(article));
            await _context.SaveChangesAsync();

            return a.CurrentValues.GetValue<int>("BlogArticleID");
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyArticleAsync(int articleId)
        {
            var article = await _context.BlogArticles.FindAsync(articleId);

            if (article is null)
            {
                return false;
            }

            _context.BlogArticles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<BlogArticle>> ShowArticlesAsync(int offset, int limit)
            => limit != -1 ? _context.BlogArticles.Skip(offset).Take(limit).Select(product => GetArticleMod(product)).ToList() : _context.BlogArticles.Skip(offset).Select(product => GetArticleMod(product)).ToList();

        /// <inheritdoc/>
        public bool TryShowArticle(int articleId, out BlogArticle article)
        {
            article = GetArticleMod(_context.BlogArticles.Find(articleId));

            if (article is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateArticleAsync(int articleId, BlogArticle article)
        {
            var art = _context.BlogArticles
                .Where(prod => prod.BlogArticleID == articleId)
                .FirstOrDefault();

            art.Title = article.Title;
            art.Text = article.Text;
            art.DatePublished = article.DatePublished;
            art.PublisherId = article.PublisherId;


            await _context.SaveChangesAsync();

            if (_context.BlogArticles.Contains(GetArticleEnt(article)))
            {
                return true;
            }

            return false;
        }

        private static BlogArticle? GetArticleMod(Entities.BlogArticle article)
        {
            return article is null ? null : new ()
            {
                Id = article.BlogArticleID,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            };
        }

        private static Entities.BlogArticle GetArticleEnt(BlogArticle article)
            => new ()
            {
                BlogArticleID = article.Id,
                Title = article.Title,
                Text = article.Text,
                DatePublished = article.DatePublished,
                PublisherId = article.PublisherId,
            };
    }
}
