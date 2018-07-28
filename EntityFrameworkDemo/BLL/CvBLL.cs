using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.IDAL;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.BLL
{
    public class CvBLL : ICvBLL
    {
        private readonly ICvDAL     _cvDal;
        private readonly LogAdapter _logger;
        private readonly UserInfo   _userInfo;

        public CvBLL(ICvDAL     cvDal,
                     LogAdapter logger,
                     UserInfo   userInfo)
        {
            _cvDal    = cvDal;
            _logger   = logger;
            _userInfo = userInfo;
            _logger.Initial(GetType().Name);
        }

        public List<CompCvVM> Get()
        {
            var entities = _cvDal.Get();
            var vm = entities.Select(c => ToCompCvVM(c))
                           .ToList();
            return vm;
        }

        public CompCvVM Get(Guid id)
        {
            var entity = _cvDal.Get(id);
            var result = ToCompCvVM(entity);
            return result;
        }

        public IEnumerable<CountryLanguage> GetCountryIdNames(string currentLanguage)
        {
            return _cvDal.GetCountryIdNames(currentLanguage);
        }

        public IEnumerable<CountyLanguage> GetCountyIdNames(string currentLanguage)
        {
            return _cvDal.GetCountyIdNames(currentLanguage);
        }

        private CompCvVM ToCompCvVM(CompCv entity)
        {
            var result = new CompCvVM();
            result.CvId      = entity.CvId;
            result.FirstName = entity.FirstName;
            result.LastName  = entity.LastName;
            result.CountryId = entity.CountryId;
            result.CountyId  = entity.CountyId;
            result.Certificates = entity.CompCvCertificates
                                        .Select(cert => ToCerttificateVM(cert))
                                        .ToList();
            result.Educations = entity.CompCvEducations
                                      .Select(edu => ToEducationVM(edu))
                                      .ToList();
            result.LanguageRequirements = entity.CompCvLanguageRequirements
                                                .Select(lang => ToLanguageRequirementVM(lang))
                                                .ToList();
            return result;
        }

        private CompCvCertificateVM ToCerttificateVM(CompCvCertificate dto)
        {
            return new CompCvCertificateVM
                   {
                       CertificateId          = dto.CertificateId,
                       CertificateName        = dto.CertificateName,
                   };
        }

        private CompCvEducationVM ToEducationVM(CompCvEducation dto)
        {
            return new CompCvEducationVM
                   {
                       EducationId          = dto.EducationId,
                       AcademyName          = dto.AcademyName,
                   };
        }

        private CompCvLanguageRequirementVM ToLanguageRequirementVM(CompCvLanguageRequirement dto)
        {
            return new CompCvLanguageRequirementVM
                   {
                       LanguageRequirementId = dto.LanguageRequirementId,
                       Language            = dto.Language,
                       Listening           = dto.Listening,
                   };
        }

        public void Add(CompCvVM cvVm)
        {
            var entity = ToCompCv(cvVm);
            _cvDal.Add(entity);
        }

        public bool Update(CompCvVM cvVm)
        {
            var entity = ToCompCv(cvVm,isUpdate: true);
            return _cvDal.Update(entity);
        }

        public bool Del(Guid id)
        {
            return _cvDal.Delete(id);
        }

        private CompCv ToCompCv(CompCvVM vm, bool isUpdate = false)
        {
            var cv = new CompCv();
            cv.CvId = isUpdate
                          ? vm.CvId
                          : Guid.NewGuid();
            cv.FirstName = vm.FirstName;
            cv.LastName = vm.LastName;
            cv.CountryId = vm.CountryId;
            cv.CountyId = vm.CountyId;
            cv.CompCvCertificates = vm.Certificates
                                      .Where(c=>string.IsNullOrWhiteSpace(c.CertificateName) == false)
                                      .Select(c =>
                                                   {
                                                       var entity = ToCompCvCerttificate(c);
                                                       entity.CertificateId = c.CertificateId ?? Guid.NewGuid();
                                                       entity.CvId = cv.CvId;
                                                       return entity;
                                                   }).ToList();
            cv.CompCvEducations = vm.Educations
                                    .Where(c => string.IsNullOrWhiteSpace(c.AcademyName) == false)
                                    .Select(c =>
                                               {
                                                   var entity = ToCompCvEducations(c);
                                                   entity.EducationId = c.EducationId ?? Guid.NewGuid();
                                                   entity.CvId = cv.CvId;
                                                   return entity;
                                               }).ToList();
            cv.CompCvLanguageRequirements = vm.LanguageRequirements
                                              .Select(c =>
                                                           {
                                                               var entity = ToCompCvLanguageRequirement(c);
                                                               entity.LanguageRequirementId = c.LanguageRequirementId ?? Guid.NewGuid();
                                                               entity.CvId = cv.CvId;
                                                               return entity;
                                                           }).ToList();
            return cv;
        }

        private CompCvCertificate ToCompCvCerttificate(CompCvCertificateVM vm)
        {
            var certificate = new CompCvCertificate();
            certificate.CertificateName = vm.CertificateName;
            return certificate;
        }

        private CompCvEducation ToCompCvEducations(CompCvEducationVM vm)
        {
            var education = new CompCvEducation();
            education.AcademyName = vm.AcademyName;
            return education;
        }

        private CompCvLanguageRequirement ToCompCvLanguageRequirement(CompCvLanguageRequirementVM vm)
        {
            var requirement = new CompCvLanguageRequirement();
            requirement.Language = vm.Language;
            requirement.Listening = vm.Listening;
            return requirement;
        }
    }
}