﻿using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {   
        private readonly ILogger<PostsController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        

        public PostsController(
            ILogger<PostsController> logger,
            IBlogRepository blogRepository,
            IMediaManager mediaManager,
            IMapper mapper )
        {   
            _logger = logger;
            _blogRepository = blogRepository;
            _mediaManager = mediaManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(PostFilterModel model)
        {
            _logger.LogInformation("Tạo điều kiện truy vấn");
            /*Sử dụng Mapster để tạo đối tưởng PostQuery
            từ đối tượng PostFileterModel model*/
            var postQuery = _mapper.Map<PostQuery>(model );

            _logger.LogInformation("Lấy danh sách bài viết từ CSDL");


            ViewBag.PostsList = await _blogRepository
                .GetPagedPostsAsync(postQuery,1,10);

            _logger.LogInformation("Chuẩn bị dữ liệu cho ViewModel");

            await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id=0)
        {
            var post = id > 0
                ? await _blogRepository.GetPostByIdAsync(id, true)
                : null;
            var model = post == null
                ? new PostEditModel()
                : _mapper.Map<PostEditModel>(post);

            await PopulatePostEditModelAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            IValidator<PostEditModel> postValidator, 
            PostEditModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePostEditModelAsync(model);
                return View(model);
            }
            var post = model.Id > 0
                ? await _blogRepository.GetPostByIdAsync(model.Id)
                :null;
            if (post == null)
            {
                post = _mapper.Map<Post>(model);
                post.Id = 0;
                post.PostedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map(model, post);

                post.Category = null;
                post.ModifiedDate = DateTime.Now;
            }

            /*Nếu người dùng có upload hình ảnh minh họa cho bài viết*/

            if (model.ImageFile?.Length > 0)
            {
                /*Thì thực hiện việc lưu tập tin vào thư mục uploads*/
                var newImagePath = await _mediaManager.SaveFileAsync(
                    model.ImageFile.OpenReadStream(),
                    model.ImageFile.FileName,
                    model.ImageFile.ContentType);
            }
            /*Nếu lưu thành công, xóa tập tin ảnh cữ (nếu có)*/
            if (!string.IsNullOrWhiteSpace(newImagePath))
            {
                await _mediaManager.DeleteFileAsync(post.ImageUrl);
                post.ImageUrl = newImagePath;
            }

            await _blogRepository.CreateOrUpdatePostAsync(
                post, model.GetSelectedTags());

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> VerifyPostSlug(
            int id, string urlSlug)
        {
            var slugExisted = await _blogRepository
                .IsPostSlugExistedAsync(id, urlSlug);

            return slugExisted
                ? Json($"Slug '{urlSlug}' đã được sử dụng")
                : Json(true);
        }
    }
    /*public class postscontroller
    {
        private readonly iblogrepository _blogrepository;

        public postscontroller(iblogrepository blogrepository)
        {
            _blogrepository = blogrepository;   
        }

        private async task populatepostfiltermodelasync(postfiltermodel model)
        {
            var authors = await _blogrepository.getauthorsasync();
            var categories = await _blogrepository.getcategoriesasync();

            model.authorlist = authors.select(a => new selectlistitem()
            {
                text = a.fullname,
                value = a.id.tostring()
            });

            model.categorylist = categories.select(c => new selectlistitem()
            {
                text = c.name,
                value = c.id.tostring()
            });
        }

        public async task<iactionresult> index(postfiltermodel model)
        {
            var postquery = new postquery()
            {
                keyword = model.keyword,
                categoryid = (int)model.categoryid,
                authorid = (int)model.authorid,
                year = (int)model.year,
                month = (int)model.month
            };
            
            viewbag.postslist = await _blogrepository
                .getpagedpostsasync(postquery, 1, 10);

            await populatepostfiltermodelasync(model);

            return view(model);
        }
    }*/
}
