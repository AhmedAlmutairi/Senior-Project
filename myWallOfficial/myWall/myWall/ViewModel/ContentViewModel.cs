﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace myWall.ViewModel
{
    public class ContentViewModel
    {
        /// <summary>
        /// Get and Set id
        /// </summary>
        public int Id { get; set; }


        public string UserId { get; set; }
        public int WallId { get; set; }
        public int CallobId { get; set; }



        /// <summary>
        /// Get and set title of content 
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Get and set Description for content
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Get and set Content for content
        /// </summary>
        [AllowHtml]
        [Required]
        public string Contents { get; set; }
        /// <summary>
        /// Images
        /// </summary>
        [Required]
        public byte[] Image { get; set; }
    }
}