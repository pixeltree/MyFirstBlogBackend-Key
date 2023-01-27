namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;

public interface IPostService
{
    IEnumerable<Post> GetPosts();
    Post GetPost(String slug);
}

public class PostService : IPostService
{
    private DataContext _context;

    public PostService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Post> GetPosts()
    {
        return _context.Posts;
    }

    public Post GetPost(string slug)
    {
        return getPost(slug);
    }

    private Post getPost(string slug)
    {
        var post = _context.Posts.Where(post => post.Slug == slug).SingleOrDefault();
        if (post == null) throw new KeyNotFoundException("Post not found");
        return post;
    }
}
