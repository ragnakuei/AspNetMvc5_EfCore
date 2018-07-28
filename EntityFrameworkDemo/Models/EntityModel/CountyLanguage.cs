using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("CountyLanguage")]
    public class CountyLanguage
    {
        [Key]
        public Guid CountyLanguageId { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public Guid CountyId { get; set; }

        [ForeignKey("CountyId")]
        public County County { get; set; }
    }
}