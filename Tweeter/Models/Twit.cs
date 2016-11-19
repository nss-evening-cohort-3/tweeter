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
        /*
         * Maybe just add a Username field here
         * Then you'll need to mock the Twit table to return List<Twit>
         * This clearly means that you need to EITHER: 
         *     1. Re-scaffold the InitialCreate migration OR 
         *     2. Create a new Migration that adds this field.
         */
    }
}