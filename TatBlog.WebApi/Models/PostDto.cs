namespace TatBlog.WebApi.Models;

public class PostDto
{
    // Mã bài viết
    public int Id { get; set; }

    // Tiêu đề bài viết
    public string Title { get; set; }

    // Mô tả hay giới thiệu ngắn về nội dung
    public string ShortDescription { get; set; }
    public string UrlSlug { get; set; }
    public string ImageUrl { get; set; }
    public int ViewCount { get; set; }
    public DateTime PostedDate { get; set; }
    public CategoryDto Category { get; set; }
    public AuthorDto Author { get; set; }
    public IList<TagDto> Tags { get; set; }
}
