using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("ActionReceiptNoteLanguage")]
    public class ActionReceiptNoteLanguage
    {
        [Key]
        public Guid ActionReceiptNoteLanguageId { get;  set; }
        public Guid     ActionReceiptId          { get; set; }
        public DateTime ActionReceiptEffectiveOn { get; set; }
        [MaxLength(10)]
        public string Language { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("ActionReceiptId")]
        public ActionReceipt ActionReceipt { get; set; }
        [ForeignKey("ActionReceiptEffectiveOn")]
        public ActionReceipt ActionReceipt1 { get; set; }
    }
}