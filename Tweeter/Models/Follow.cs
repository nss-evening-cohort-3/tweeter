using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class Follow
    {
        [Key]
        public int FollowId { get; set; }
        public Twit TwitFollower { get; set; }
        public Twit TwitFollowed { get; set; }
    }

    /* Also correct
      public class Follow
    {
        [Key]
        public int FollowId { get; set; }
        public int TwitFollowerId { get; set; }
        public int TwitFollowedId { get; set; }
    }
    */
}
 