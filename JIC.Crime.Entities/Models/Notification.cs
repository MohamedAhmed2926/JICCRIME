namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Body { get; set; }

        public DateTime NotificationDate { get; set; }

        public byte NotificationActionID { get; set; }

        public long? CaseID { get; set; }

        public long? CaseSessionID { get; set; }

        public long NotificationActionBy { get; set; }

        public long NotifierID { get; set; }

        public bool IsViewer { get; set; }

        public bool BeenRead { get; set; }

        public string URL { get; set; }

        public virtual CaseAction CaseAction { get; set; }
    }
}
