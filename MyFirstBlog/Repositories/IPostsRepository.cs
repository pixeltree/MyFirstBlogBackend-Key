namespace MyFirstBlog.Repositories;

using MyFirstBlog.Entities;

public interface IPostsRepository
{
    IEnumerable<Post> GetPosts();
    Post GetPost(String slug);
}
