using DomainLayer.Entities;
using Microsoft.Extensions.Logging;
using SportMap.DAL.Abstractions.Repositories;
using SportMap.DAL.Common;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.Repositories
{
    public class PostRepository(AppDbContext context, ILogger<PostRepository> logger) : BaseRepository<Post>(context, logger), IPostRepository;
}
