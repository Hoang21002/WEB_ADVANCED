﻿using FluentValidation;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class PostValidator :AbstractValidator<PostEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public PostValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(500);


            RuleFor(x => x.ShortDescription)
                .NotEmpty();


            RuleFor(x => x.Description)
                .NotEmpty();
                


            RuleFor(x => x.Meta)
                .NotEmpty()
                .MaximumLength(1000);


            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .MaximumLength(1000);


            RuleFor(x => x.Title)
                .MustAsync(async (postModel, slug, cancellationToken) =>
                !await blogRepository.IsPostSlugExistedAsync(
                    postModel.Id, slug, cancellationToken))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");


            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn chủ đề cho bài viết");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn tác giả của bài viết");

            RuleFor(x => x.SelectedTags)
                .NotEmpty()
                .WithMessage("Bạn phải nhập ít nhất một thẻ");

            When(x => x.Id <= 0, () =>
            {
                RuleFor(x => x.ImageFile)
                .Must(x => x is { Length: > 0 })
                .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
            })
             .Otherwise(() =>
             {
                 RuleFor(x => x.ImageFile)
                .MustAsync(SetImageIfNotExist)
                .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
             });
        }
        /*Kiểm tra xem người dùng đã nhập ít nhất 1 thẻ (tag)*/
        private bool HasAtLeastOneTag(
            PostEditModel postModel, string selectedTags)
        {
            return postModel.GetSelectedTags().Any();
        }

        /*Kiểm tra xem bài viết đã có hình ảnh chưa.
        Nếu chưa có, bắt buộc người dùng phảo chọn file.*/

        private async Task<bool> SetImageIfNotExist(
            PostEditModel postModel,
            IFormFile imageFile,
            CancellationToken cancellationToken)
        {
            /*Lấy thong tin bài viết từ CSDL*/
            var post = await _blogRepository.GetPostByIdAsync(
                postModel.Id, false, cancellationToken);

            /*Nếu bài viết đã có hình ảnh => không bắt buộc chọn file*/
            if (!string.IsNullOrWhiteSpace(post?.ImageUrl))
                return true;

            /*Ngược lại (bài viết chưa có hình ảnh), kiểm tra xem
            người dùng đã chọn file hay chưa. Nếu chưa thì báo lỗi*/
            return imageFile is { Length: > 0 };
        }
    }
}
