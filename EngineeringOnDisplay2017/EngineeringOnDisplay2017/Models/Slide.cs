using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringOnDisplay2017.Models
{
    public class Slide
    {
        [Key]
        public int Id { get; set; }  //id for this slide
        public string FullUrl { get; set; } //url for the full image
        public string ThumbUrl { get; set; } //url for the thumbnail image
        public int TimeSeconds { get; set; } //how long the slide will be displayed
        public int Order { get; set; }  //order in the slide show.  Will work best if it does not have duplicates
    }
}
