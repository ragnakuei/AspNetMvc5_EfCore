using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models.EntityModel
{
    [Table("CompCvCertificate")]
    public class CompCvCertificate
    {
        [Key]
        public Guid CertificateId { get; set; }

        public Guid CvId { get; set; }

        [MaxLength(50)]
        public string CertificateName { get; set; }

        [ForeignKey("CvId")]
        public CompCv CompCv { get; set; }
    }
}