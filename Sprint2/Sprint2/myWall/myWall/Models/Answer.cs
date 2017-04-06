namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public int Id { get; set; }

        [Column("Answer")]
        [Required]
        [StringLength(500)]
        public string Answer1 { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(128)]
        public string Votes { get; set; }

        public virtual Post Post { get; set; }
    }
}
