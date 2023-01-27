namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using System.Text.RegularExpressions;
using MyFirstBlog.Dtos;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(String slug);
}

public class PostService : IPostService
{
    private DataContext _context;

    public PostService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<PostDto> GetPosts()
    {
        return _context.Posts.Select(post => post.AsDto());
    }

    public PostDto GetPost(string slug)
    {
        return getPost(slug).AsDto();
    }

    private Post getPost(string slug)
    {
        return _context.Posts.Where(a=>a.Slug==slug.ToString()).SingleOrDefault();
    }
}
