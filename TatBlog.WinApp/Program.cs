﻿using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

//test push
/*var context = new BlogDbContext();*/
var context = new BlogDbContext();

//Tên và chủ đề (2)
//var posts = context.Posts
//    .Where(p => p.Published)
//    .OrderBy(p => p.Title)
//    .Select(p => new
//    {
//        Id = p.Id,
//        Title = p.Title,
//        ViewCount = p.ViewCount,
//        PostedDate = p.PostedDate,
//        Author = p.Author.FullName,
//        Category = p.Category.Name,
//    })
//    .ToList();

//foreach (var post in posts)
//{
//    Console.WriteLine("id      :{0}", post.Id);
//    Console.WriteLine("title   :{0}", post.Title);
//    Console.WriteLine("view    :{0}", post.ViewCount);
//    Console.WriteLine("date    :{0:mm/dd/yyyy}", post.PostedDate);
//    Console.WriteLine("author  :{0}", post.Author);
//    Console.WriteLine("category:{0}", post.Category);
//    Console.WriteLine("".PadRight(80, '-'));
//}


// Tìm 3 bài viết được xem/đọc nhiều nhất (3)
//IBlogRepository blogRepo = new BlogRepository(context);

//var posts = await blogRepo.GetPopularArticlesAsync(3);


//foreach (var post in posts)
//{
//    Console.WriteLine("id      :{0}", post.Id);
//    Console.WriteLine("title   :{0}", post.Title);
//    Console.WriteLine("view    :{0}", post.ViewCount);
//    Console.WriteLine("date    :{0:mm/dd/yyyy}", post.PostedDate);
//    Console.WriteLine("author  :{0}", post.Author);
//    Console.WriteLine("category:{0}", post.Category);
//    Console.WriteLine("".PadRight(80, '-'));
//}



// Tên tác giả (1)
//
var seeder = new DataSeeder(context);

seeder.Initialize();

var authors = context.Authors.ToList();


Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}", "ID", "Full Name", "Email", "Joined Date");

foreach (var author in authors)
{
    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}",
author.Id, author.FullName, author.Email, author.JoinedDate);
}


// Lấy danh sách chuyên mục (4)
//IBlogRepository blogRepo = new BlogRepository(context);

//var categories = await blogRepo.GetCategoriesAsync();

//Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
//foreach (var item in categories)
//{
//    Console.WriteLine("{0,-5}{1,-50}{2,10}",item.Id, item.Name, item.PostCount);
//}

// Lấy danh sách từ khóa (5)
/*IBlogRepository blogRepo = new BlogRepository(context);

var pagingParams = new PagingParams
{
    PageNumber = 1,
    PageSize = 5,
    SortColumn = "Name",
    SortOrder = "DESC"
};

var tagsList = await blogRepo.GetPagedTagsAsync(pagingParams);

Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach (var item in tagsList)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}", item.Id, item.Name, item.PostCount);
}*/