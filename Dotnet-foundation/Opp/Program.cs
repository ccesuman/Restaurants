using Encapsulation;

var manager = new PostManager();

manager.CreatePost("What is C#?", "Basic explanation needed");
manager.CreatePost("Encapsulation in OOP", "Explain with example");

Console.WriteLine("All Posts:");
foreach (var post in manager.GetAllPosts())
{
    Console.WriteLine($"{post.Id}. {post.Title} (Score: {post.GetScore()})");
}

Console.WriteLine("\nDeleting post with Id = 1");
manager.DeletePost(1);

Console.WriteLine("\nRemaining Posts:");
foreach (var post in manager.GetAllPosts())
{
    Console.WriteLine($"{post.Title}");
}

Console.ReadLine();
 
