using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation 
{
    public class StackOverflowPost
    {
        private int _upVotes;
        private int _downVotes;

        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime CreatedOn { get; }

        public StackOverflowPost(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedOn = DateTime.Now;
        }

        public void UpVote() => _upVotes++;
        public void DownVote() => _downVotes++;

        public int GetScore() => _upVotes - _downVotes;
    }
}
