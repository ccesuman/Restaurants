using Encapsulation;

public class PostManager
{
    private readonly List<StackOverflowPost> _posts = new();
    private int _idCounter = 1;

    public StackOverflowPost CreatePost(string title, string description)
    {
        var post = new StackOverflowPost(_idCounter++, title, description);
        _posts.Add(post);
        return post;
    }

    public List<StackOverflowPost> GetAllPosts()
    {
        return _posts;
    }

    public bool DeletePost(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
            return false;

        _posts.Remove(post);
        return true;
    }
}
