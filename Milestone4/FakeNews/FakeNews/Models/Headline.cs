namespace FakeNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Headline")]
    public partial class Headline
    {
        public int ID { get; set; }

        public int GenreID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Body { get; set; }

        [Required]
        [StringLength(300)]
        public string Comment { get; set; }

        [Column(TypeName = "date")]
        public DateTime PDate { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual User User { get; set; }
    }
}
