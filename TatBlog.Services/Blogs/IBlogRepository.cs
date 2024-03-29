﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{
    Task<Post> GetPostAsync(int year, int month, string slug, 
        CancellationToken cancellationToken = default);

    Task<IList<Post>> GetPopularArticlesAsync(int numPosts, 
        CancellationToken cancellationToken = default);

    Task<bool> IsPostSlugExistedAsync(int postId, string slug,
        CancellationToken cancellationToken= default);

    Task IncreaseViewCountAsync(int postId,
        CancellationToken cancellationToken = default);
/*    Task<IList<CategoryItem>> GetCategoriesAsync(bool showOnMenu = false,
        CancellationToken cancellationToken = default);*/

    //Task<Author> GetAuthorByIdAsync(int authorId);

    //Task<Author> GetAuthorAsync(string slug, CancellationToken cancellationToken = default);

    //Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default);


    // Lấy danh sách từ khóa/thẻ và phân trang theo các tham số pagingParams
    Task<IPagedList<TagItem>> GetPagedTagsAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);

    Task<IPagedList<Post>> GetPagedPostsAsync(
            PostQuery condition,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);
    Task<Post> GetPostByIdAsync(
    int postId, bool includeDetails = false,
    CancellationToken cancellationToken = default);

    Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default);

    Task<Tag> GetTagAsync(
       string slug, CancellationToken cancellationToken = default);

    Task<IList<TagItem>> GetTagsAsync(
       CancellationToken cancellationToken = default);
    Task<IPagedList<T>> GetPagedPostsAsync<T>(
    PostQuery condition,
    IPagingParams pagingParams,
    Func<IQueryable<Post>, IQueryable<T>> mapper);

}
