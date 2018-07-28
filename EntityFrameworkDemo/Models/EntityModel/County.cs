using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("County")]
    public class County
    {
        public County()
        {
            this.CompCvs         = new List<CompCv>();
            this.CountyLanguages = new List<CountyLanguage>();
        }

        [Key]
        public Guid CountyId { get;  set; }
        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get;                      set; }
        public List<CompCv>         CompCvs         { get; set; }
        public List<CountyLanguage> CountyLanguages { get; set; }
    }
}