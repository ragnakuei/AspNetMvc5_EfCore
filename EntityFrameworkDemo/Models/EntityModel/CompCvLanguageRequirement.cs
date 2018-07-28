using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("CompCvLanguageRequirement")]
    public class CompCvLanguageRequirement
    {
        [Key]
        public Guid LanguageRequirementId { get; set; }

        public Guid CvId      { get; set; }
        public byte Language  { get; set; }
        public byte Listening { get; set; }

        [ForeignKey("CvId")]
        public CompCv CompCv { get; set; }
    }
}