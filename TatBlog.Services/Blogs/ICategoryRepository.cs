using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs;

public interface ICategoryRepository
{
    Task<Category> GetCategoryAsync(
    string slug, CancellationToken cancellationToken = default);
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task<IList<CategoryItem>> GetCategoriesAsync(
       bool showOnMenu = false,
       CancellationToken cancellationToken = default);
    Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);
    Task<Category> CreateOrUpdateCategoryAsync(
        Category category, CancellationToken cancellationToken = default);
    Task<bool> IsCategorySlugExistedAsync(
        int categoryId, string categorySlug,
        CancellationToken cancellationToken = default);
    Task<bool> DeleteCategoryAsync(
    int categoryId, CancellationToken cancellationToken = default);

}
