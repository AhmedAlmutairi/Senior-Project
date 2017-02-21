namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Annotation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        public int DiagramId { get; set; }

        [Required]
        [StringLength(128)]
        public string Votes { get; set; }

        public virtual Diagram Diagram { get; set; }
    }
}
