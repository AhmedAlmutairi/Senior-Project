namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int? WallId { get; set; }

        public DateTime? Time { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [MaxLength(1)]
        public byte[] File { get; set; }

        public int? Code { get; set; }

        [Required]
        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(256)]
        public string ConnectionId { get; set; }
    }
}
