using System;
using System.Collections.Generic;

namespace EntityFrameworkDemo.Models.ViewModel
{
    public class CompCvVM
    {
        public Guid   CvId      { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public Guid   CountryId { get; set; }
        public Guid   CountyId  { get; set; }

        public List<CompCvCertificateVM>         Certificates         { get; set; }
        public List<CompCvEducationVM>           Educations           { get; set; }
        public List<CompCvLanguageRequirementVM> LanguageRequirements { get; set; }
    }

    public class CompCvCertificateVM
    {
        public Guid?  CertificateId   { get; set; }
        public string CertificateName { get; set; }
    }

    public class CompCvLanguageRequirementVM
    {
        public Guid? LanguageRequirementId { get; set; }
        public byte  Language              { get; set; }
        public byte  Listening             { get; set; }
    }

    public class CompCvEducationVM
    {
        public Guid?  EducationId { get; set; }
        public string AcademyName { get; set; }
    }
}