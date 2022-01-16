using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Blogging;

namespace NorthwindWebApps.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class CommentsController : Controller
    {
        private ICommentingService _service;

        private IBloggingService _bloggingService;

        public CommentsController(ICommentingService service, IBloggingService bloggingService)
            => (_service, _bloggingService) = (service, bloggingService);

        // POST api/articles/1/comments
        [HttpPost("{article_id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(int article_id, BlogComment comment)
        {
            BlogArticle article;

            if (!_bloggingService.TryShowArticle(article_id, out article))
            {
                return BadRequest();
            }

            comment.ArticleId = article_id;

            int? commentId = await _service.CreateCommentAsync(comment);

            if (commentId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(CreateAsync), new { id = commentId }, comment);
        }

        // GET api/articles/2/comments
        [HttpGet("{article_id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlogCommentsShow>>> GetAsync(int article_id)
        {
            BlogArticle article;

            if (!_bloggingService.TryShowArticle(article_id, out article))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var ans = comments.Where(comment => comment.ArticleId == article_id).Select(comment => GetComment(comment, article.Title)) .ToList();

            return ans;

            BlogCommentsShow GetComment(BlogComment comment, string articleTitle)
                => new()
                {
                    Id = comment.Id,
                    ArticleId = comment.ArticleId,
                    ArticleName = articleTitle,
                    Comment = comment.Comment,
                };
        }

        // DELETE api/articles/2/comments/2
        [HttpDelete("{article_id}/comments/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int article_id, int id)
        {
            BlogArticle art;

            if (!_bloggingService.TryShowArticle(article_id, out art))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var com = comments.Where(comment => comment.ArticleId == article_id).ToList()[id - 1];

            await _service.DestroyCommentAsync(com.Id);

            return NoContent();
        }


        // PUT api/articles/2/comments/2
        [HttpPut("{article_id}/comments/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int article_id, int id, BlogComment comment)
        {
            BlogArticle art;

            if (!_bloggingService.TryShowArticle(article_id, out art))
            {
                return BadRequest();
            }

            var comments = await _service.ShowCommentsAsync(0, int.MaxValue);
            var com = comments.Where(_comment => _comment.ArticleId == article_id).ToList()[id - 1];

            com.Comment = comment.Comment;

            await _service.UpdateCommentAsync(com.Id, com);

            return NoContent();
        }
    }
}
