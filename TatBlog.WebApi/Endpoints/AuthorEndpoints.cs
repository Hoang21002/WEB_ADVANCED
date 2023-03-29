using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints;

public static class AuthorEndpoints
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

    private static async Task<IResult> GetAuthors(
        [AsParameters] AuthorFilterModel model,
        IAuthorRepository authorRepository)
    {
        var authorsList = await authorRepository
            .GetPagedAuthorsAsync(model, model.Name);

        var paginationResult =
            new PaginationResult<AuthorItem>(authorsList);

        return Results.Ok(paginationResult);
    }

    private static async Task<IResult> GetAuthorDetails(
        int id,
        IAuthorRepository authorRepository,
        IMapper mapper)
    {
        var author = await authorRepository.GetCachedAuthorByIdAsync(id);
        return author == null
            ? Results.NotFound($"Không tìm thấy tác giả có mã số {id}")
            : Results.Ok(mapper.Map<AuthorItem>(author));
    }

    private static async Task<IResult> GetPostsByAuthorId(
        int id,
        [AsParameters] PagingModel pagingModel,
        IBlogRepository blogRepository)
    {
        var postQuery = new PostQuery()
        {
            AuthorId = id,
            PublishedOnly = true,
        };

        var postsList = await blogRepository.GetPagedPostsAsync(
            postQuery, pagingModel,
            posts => posts.ProjectToType<PostDto>());

        var paginationResult = new PaginationResult<PostDto>(postsList);

        return Results.Ok(paginationResult);
    }

    private static async Task<IResult> GetPostsByAuthorSlug(
        [FromRoute] string slug,
        [AsParameters] PagingModel pagingModel,
        IBlogRepository blogRepository)
    {
        var postQuery = new PostQuery()
        {
            AuthorSlug = slug,
            PublishedOnly = true,
        };
        
        var postsList = await blogRepository.GetPagedPostsAsync(
            postQuery, pagingModel,
            posts => posts.ProjectToType<PostDto>());

        var paginationResult = new PaginationResult<PostDto>(postsList);

        return Results.Ok(paginationResult);
    }

    private static async Task<IResult> AddAuthor(
        AuthorEditModel model,
        IValidator<AuthorEditModel> validator,
        IAuthorRepository authorRepository,
        IMapper mapper)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(
                validationResult.Errors.ToResponse());
        }

        if (await authorRepository
                .IsAuthorSlugExistedAsync(0, model.UrlSlug))
        {
            return Results.Conflict(
                $"Slug '{model.UrlSlug}' đã được sử dụng");
        }

        var author = mapper.Map<Author>(model);
        await authorRepository.AddOrUpdateAsync(author);

        return Results.CreatedAtRoute(
            "GetAuthorById", new { author.Id },
            mapper.Map<AuthorItem>(author));
    }


}


