using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository) 
        { 
            _blogRepository = blogRepository;
        }
        /*Action nay xu ly HTTP request den trang chu cua ung dung web
            hoac tim kiem bai viet theo tu khoa*/

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10) 
        {
            /*Tao doi tuong chua cac dieu kien truy van*/
            var postQuery = new PostQuery()
            {
                /*Chi lay nhung bai viet co trang thai Published*/
                PublishedOnly = true,

                /*Tim bai viet theo tu khoa*/
                Keyword = keyword
            };
            var postsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);


            ViewBag.PostQuery = postQuery;
            return View(postsList);
        }

        public IActionResult About()
            => View();
        public IActionResult Contact() 
            => View();
        public IActionResult Rss() 
            => Content("Nội dung sẽ được cập nhật");
        public IActionResult Category()
            => View();
        public IActionResult Author()
          => View();
        public IActionResult Tag()
          => View();
        public IActionResult Post()
          => View();
    }
}
