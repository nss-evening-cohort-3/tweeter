using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class Twit
    {
        public int TwitId { get; set; }
        public ApplicationUser BaseUser { get; set; }
        public List<Twit> Follows { get; set; }
    }
}