using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders;

public class DataSeeder : IDataSeeder
{
    private readonly BlogDbContext _dbContext;

    public DataSeeder(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {
        _dbContext.Database.EnsureCreated();
        if (_dbContext.Posts.Any()) return;

        var authors = AddAuthors();
        var categories = AddCategories();
        var tags = AddTags();
        var posts = AddPosts(authors, categories, tags);
    }

    private IList<Author> AddAuthors()
    {
        var authors = new List<Author>()
        {
            new()
            {
                FullName = "Jason Mouth",
                UrlSlug = "jason-mouth",
                Email = "json@gmail.com",
                JoinedDate = new DateTime(2022, 10, 21)
            },

            new()
            {
                FullName = "Jessica Wonder",
                UrlSlug = "jessica-wonder",
                Email = "jessica665@motip.com",
                JoinedDate = new DateTime(2020, 4, 19)
            },
            new()
            {
                FullName = "Jason Boy",
                UrlSlug = "jason-boy",
                Email = "jsonboy@gmail.com",
                JoinedDate = new DateTime(2022, 9, 20)
            },
            new()
            {
                FullName = "Jason Girl",
                UrlSlug = "jason-Girl",
                Email = "jsongirl@gmail.com",
                JoinedDate = new DateTime(2022, 8, 18)
            },
            new()
            {
                FullName = "Jason Hat",
                UrlSlug = "jason-Hat",
                Email = "jsonhat@gmail.com",
                JoinedDate = new DateTime(2022, 7, 7)
            },
            new()
            {
                FullName = "Jason Top",
                UrlSlug = "jason-top",
                Email = "jsontop@gmail.com",
                JoinedDate = new DateTime(2022, 6, 6)
            },
            new()
            {
                FullName = "Jason Bot",
                UrlSlug = "jason-bot",
                Email = "jsonbot@gmail.com",
                JoinedDate = new DateTime(2022, 5, 5)
            }
        };
        _dbContext.Authors.AddRange(authors);
        _dbContext.SaveChanges();

        return authors;
    }
    private IList<Category> AddCategories()
    {
        var categories = new List<Category>()
        {
            new() {Name =".NET Core", Description = ".NET Core", UrlSlug="Categories1"},
            new() {Name ="Architecture", Description = "Architecture", UrlSlug="Categories2"},
            new() {Name ="Messaging", Description = "Messaging", UrlSlug="Categories3"},
            new() {Name ="OOP", Description = "Object-Oriented Program", UrlSlug="Categories4"},
            new() {Name ="Design Patterns", Description = "Design Patterns", UrlSlug="Categories5"}
        };

        _dbContext.AddRange(categories);
        _dbContext.SaveChanges();

        return categories;
    }

    private IList<Tag> AddTags()
    {
        var tags = new List<Tag>()
        {
            new() {Name ="Google", Description = "Google applications", UrlSlug="Tag1"},
            new() {Name ="ASP.NET MVC", Description = "ASP.NET MVC", UrlSlug="Tag2"},
            new() {Name ="Razor Page", Description = "Razor page", UrlSlug="Tag3"},
            new() {Name ="Blazor", Description = "Blazor", UrlSlug="Tag4"},
            new() {Name ="Deep Learning", Description = "Deep Learning", UrlSlug="Tag5"},
            new() {Name ="Neural Network", Description = "Neural Network", UrlSlug="Tag6"}
        };
        _dbContext.AddRange(tags);
        _dbContext.SaveChanges();
        return tags;


    }
    private IList<Post> AddPosts(
        IList<Author> authors,
        IList<Category> categories,
        IList<Tag> tags)
    {
        var posts = new List<Post>()
        {
            new()
            {
                Title = "ASP.NET Core Diagnostic Scenarios",
                ShortDescription = "David and friends has a great repos",
                Description = "Here's a few great DON'T and DO examples",
                Meta ="David and friends has a great repository filled",
                UrlSlug = "aspnet-core-diagnostic-scenarios",
                Published = true,
                PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[0],
                Category = categories[0],
                Tags = new List<Tag>()
                {
                    tags[0]
                }
            }
        };
        //    new()
        //    {
        //        Title = "ABS.NET Core Diagnostic Scenarios",
        //        ShortDescription = "David and friends has a great repos",
        //        Description = "Here's a few great DON'T and DO examples",
        //        Meta ="David and friends has a great repository filled",
        //        UrlSlug = "aspnet-core-diagnostic-scenarios",
        //        Published = true,
        //        PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
        //        ModifiedDate = null,
        //        ViewCount = 10,
        //        Author = authors[1],
        //        Category = categories[1],
        //        Tags = new List<Tag>()
        //        {
        //            tags[1]
        //        }
        //    }
        //};
        _dbContext.AddRange(posts);
        _dbContext.SaveChanges();

        return posts;
    }

}


