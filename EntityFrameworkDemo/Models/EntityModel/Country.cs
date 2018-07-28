using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("Country")]
    public class Country
    {
        public Country()
        {
            this.CompCvs          = new List<CompCv>();
            this.CountryLanguages = new List<CountryLanguage>();
            this.Counties         = new List<County>();
        }

        [Key]
        public Guid CountryId { get; set; }
        [MaxLength(4)]
        public string Code { get; set; }

        public List<CompCv>          CompCvs          { get; set; }
        public List<CountryLanguage> CountryLanguages { get; set; }
        public List<County>          Counties         { get; set; }
    }
}