namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using System.Text.RegularExpressions;
using MyFirstBlog.Dtos;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(String slug);
    Task<PostDto> AddPost(PostDto post);
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

    public async Task<PostDto> AddPost(PostDto post)
    {
        Post newPost = await addPost(post);
        return newPost.AsDto();
    }

    private Post getPost(string slug)
    {
        return postsWithSlug(slug).SingleOrDefault();
    }

    private async Task<Post> addPost(PostDto post)
    {
        validateSlug(post.Slug);
        var newPost = new Post() {
            Title = post.Title,
            Slug = post.Slug,
            Body = post.Body,
            CreatedDate = DateTime.UtcNow
        };
        _context.Add(newPost);
        await _context.SaveChangesAsync();

        return getPost(newPost.Slug);
    }

    private void validateSlug(String slug)
    {
        validateSlugCharacters(slug);
        if(!postsWithSlug(slug).Any()) return;

        throw new ArgumentException(String.Format("The Slug {0} has already been used", slug), "slug");
    }

    private IEnumerable<Post> postsWithSlug(String slug)
    {
        return _context.Posts.Where(a=>a.Slug==slug.ToString());
    }

    private void validateSlugCharacters(String slug)
    {
        string illegalChars = @"[^a-zA-Z\d-_]";
        var illegalMatches = Regex.Match(slug, illegalChars, RegexOptions.IgnoreCase);

        if (illegalMatches.Success) throw new ArgumentException("Invalid characters in slug");
    }
}
