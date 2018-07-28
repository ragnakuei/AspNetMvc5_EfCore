using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("CountryLanguage")]
    public class CountryLanguage
    {
        [Key]
        public Guid CountryLanguageId { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}