using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("ActionReceipt")]
    public class ActionReceipt
    {
        public ActionReceipt()
        {
            this.ActionReceiptNoteLanguages  = new List<ActionReceiptNoteLanguage>();
            this.ActionReceiptNoteLanguages1 = new List<ActionReceiptNoteLanguage>();
        }

        [Key]
        [Column(Order = 1)]
        public Guid ActionReceiptId { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime EffectiveOn { get;            set; }
        public DateTime  CreateOn              { get; set; }
        public DateTime  UpdateOn              { get; set; }
        public Guid?     ParentActionReceiptId { get; set; }
        public DateTime? ParentEffectiveOn     { get; set; }

        [InverseProperty("ActionReceipt")]
        public List<ActionReceiptNoteLanguage> ActionReceiptNoteLanguages { get; set; }
        [InverseProperty("ActionReceipt1")]
        public List<ActionReceiptNoteLanguage> ActionReceiptNoteLanguages1 { get; set; }
    }
}