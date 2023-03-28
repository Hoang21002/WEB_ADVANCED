using TatBlog.Core.Collections;
using TatBlog.Core.DTO;

namespace TatBlog.WebApi.Endpoints
{
    public static WebApplication MapAuthorEndpoints(
     this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api/authors");

        routeGroupBuilder.MapGet("/", GetAuthors)
            .WithName("GetAuthors")
            .Produces<PaginationResult<AuthorItem>>();

        return app;
    }
}
