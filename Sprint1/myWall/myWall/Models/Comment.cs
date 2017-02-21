namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int PostId { get; set; }

        [Column("Comment")]
        [Required]
        [StringLength(500)]
        public string Comment1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Vote { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Post Post { get; set; }
    }
}
