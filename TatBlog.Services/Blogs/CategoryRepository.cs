using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Blogs;

public class CategoryRepository : ICategoryRepository
{
    private readonly BlogDbContext _context;
    private readonly IMemoryCache _memoryCache;
    public CategoryRepository (BlogDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<Category> GetCategoryAsync(
    string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .FirstOrDefaultAsync(x => x.UrlSlug == slug, cancellationToken);
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Set<Category>().FindAsync(categoryId);
    }

    public async Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();

        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu);
        }

        return await categories
            .OrderBy(x => x.Name)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default)
    {
        var tagQuery = _context.Set<Category>()
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            });

        return await tagQuery.ToPagedListAsync(pagingParams, cancellationToken);
    }

    public async Task<Category> CreateOrUpdateCategoryAsync(
        Category category, CancellationToken cancellationToken = default)
    {
        if (category.Id > 0)
        {
            _context.Set<Category>().Update(category);
        }
        else
        {
            _context.Set<Category>().Add(category);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<bool> IsCategorySlugExistedAsync(
        int categoryId, string categorySlug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .AnyAsync(x => x.Id != categoryId && x.UrlSlug == categorySlug, cancellationToken);
    }

    public async Task<bool> DeleteCategoryAsync(
    int categoryId, CancellationToken cancellationToken = default)
    {
        var category = await _context.Set<Category>().FindAsync(categoryId);

        if (category is null) return false;

        _context.Set<Category>().Remove(category);
        var rowsCount = await _context.SaveChangesAsync(cancellationToken);

        return rowsCount > 0;
    }
}
