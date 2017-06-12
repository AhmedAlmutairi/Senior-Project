namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dolist")]
    public partial class Dolist
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? WallId { get; set; }

        public DateTime? Time { get; set; }

        [MaxLength(1)]
        public byte[] File { get; set; }

        [StringLength(500)]
        public string Item { get; set; }

        [StringLength(500)]
        public string Status { get; set; }
    }
}
