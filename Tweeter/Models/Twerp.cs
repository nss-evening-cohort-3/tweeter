using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class Twerp
    {
        public int TwerpId { get; set; }
        public string Message { get; set; }
        public Twit Author { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Twerp> Replies { get; set; }
    }
}