using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string Message { get; set; }
        public Twit Author { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Tweet> Replies { get; set; }  // Self referential
          
    }
}