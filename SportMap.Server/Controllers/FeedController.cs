using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportMap.AL.DTOs;
using SportMap.AL.UseCases.Feeds;
using SportMap.AL.UseCases.Feeds.GetFeeds;
using SportMap.PL.Common;
using SportMap.PL.Extensions;
using StatusType = DomainLayer.Entities.Enums.StatusType;

namespace SportMap.PL.Controllers
{
    [Route("api/feed")]
    [ApiController]
    public class FeedController(
        GetPostQueryHandler getPosts,
        CreatePostCommandHandler createPosts,
        ILogger<FeedController> logger) : BaseController<PostDTO>(logger)
    {
        // GET: api/feed
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<PostDTO>>> Get()
        {
            var query = new GetPostQuery(null, StatusType.Verified);
            var result = await getPosts.Handle(query, CancellationToken.None);

            if (result.HasError)
            {
                _logger.LogError("{controllerName}.{methodName}: Error occurred while fetching posts: {ErrorMessage}", nameof(FeedController), nameof(Get), result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }

            var posts = result.Data;

            if (posts!.Count == 0)
            {
                _logger.LogWarning("{controllerName}.{methodName}: No posts found", nameof(FeedController), nameof(Get));
                return NotFound();
            }

            return Ok(posts);
        }

        // GET: api/feed
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDTO>> Get(Guid id)
        {
            var query = new GetPostQuery(null, StatusType.Verified);
            var result = await getPosts.Handle(query, CancellationToken.None);

            if (result.HasError)
            {
                _logger.LogError("{controllerName}.{methodName}: Error occurred while fetching posts: {ErrorMessage}", nameof(FeedController), nameof(Get), result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }

            var posts = result.Data;

            if (posts!.Count == 0)
            {
                _logger.LogWarning("{controllerName}.{methodName}: No posts found", nameof(FeedController), nameof(Get));
                return NotFound();
            }

            return Ok(posts[0]);
        }

        // POST: api/feed
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostRequest request)
        {
            if (request.Title.IsNullOrEmpty() || request.Content.IsNullOrEmpty())
            {
                _logger.LogWarning("Title or content is null or empty");

                return BadRequest("Title and content cannot be null or empty");
            }

            var command = new CreatePostCommand(request.Title, request.Content);
            var result = await createPosts.Handle(command, CancellationToken.None);

            if (result.HasError)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(CreatePost), new { id = result.Data.Id }, result.Data);
        }
    }

    public class CreatePostRequest(string title, string content)
    {
        public string Title { get; init; } = title;
        public string Content { get; init; } = content;
    }
}
