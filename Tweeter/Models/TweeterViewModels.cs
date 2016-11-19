using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tweeter.Models
{
    public class TweeterViewModels
    {
        [Required]
        [MinLength(2)]
        public string Message { get; set; }
        public string ImageURL { get; set; }
    }
}