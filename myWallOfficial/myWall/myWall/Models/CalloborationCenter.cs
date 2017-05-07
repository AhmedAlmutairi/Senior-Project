namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CalloborationCenter")]
    public partial class CalloborationCenter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Tier { get; set; }

        public int CallobId { get; set; }

        public virtual CalloborationAccount CalloborationAccount { get; set; }
    }
}
