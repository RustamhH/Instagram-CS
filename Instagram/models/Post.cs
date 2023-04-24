using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram
{
    namespace PostNamespace
    {
        internal sealed class Post
        {

            public bool IsViewed { get; set; }
            public bool IsLiked { get; set; }
            
            public readonly string Id;
            public string Title { get; set; }
            public string Content { get; set; }
            
            public readonly DateTime CreationTime;

            public uint LikeCount { get; set; }
            public uint ViewCount { get; set; }


            public Post() { Id = Guid.NewGuid().ToString().Substring(0, 5); }

            public Post(string content, string title, DateTime creationTime) : this()
            {
                Content = content;
                Title = title;
                CreationTime = creationTime;
            }


            public override string ToString() => $"Title: {Title}\nContent: {Content}\nTime Posted: {CreationTime}\nLike Count: {LikeCount}\nView Count: {ViewCount}\n";

            public string GetShortInfo() => $"Id: {Id}\nTitle: {Title}\n";
        }
    }
    
}
