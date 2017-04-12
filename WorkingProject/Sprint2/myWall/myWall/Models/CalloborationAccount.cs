namespace myWall.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CalloborationAccount")]
    public partial class CalloborationAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CalloborationAccount()
        {
            CalloborationCenters = new HashSet<CalloborationCenter>();
            UserCalloborations = new HashSet<UserCalloboration>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Organization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalloborationCenter> CalloborationCenters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCalloboration> UserCalloborations { get; set; }
    }
}
