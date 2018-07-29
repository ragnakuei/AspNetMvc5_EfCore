using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.IDAL;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkDemo.BLL
{
    public class CountyBLL : ICountyBLL
    {
        private readonly ICountyDAL         _countyDal;
        private readonly ICountryDAL        _countryDal;
        private readonly ILogger<CountyBLL> _logger;
        private readonly UserInfo           _userInfo;

        public CountyBLL(ICountyDAL         countyDal,
                         ICountryDAL        countryDal,
                         ILogger<CountyBLL> logger,
                         UserInfo           userInfo)
        {
            _countyDal  = countyDal;
            _countryDal = countryDal;
            _logger     = logger;
            _userInfo   = userInfo;
        }

        public List<CountyVM> Get()
        {
            var entities = _countyDal.Get()
                               .Select(c => ToCountyVM(c))
                               .ToList();
            return entities;
        }

        private CountyVM ToCountyVM(County entity)
        {
            var result = new CountyVM();
            result.Id = entity.CountyId;

            var countyLanguage = entity.CountyLanguages
                                       ?.FirstOrDefault(cl=>cl.Language == _userInfo.CurrentLanguage);
            result.Language = _userInfo.CurrentLanguage;
            if (countyLanguage != null)
            { 
                result.LanguageId = countyLanguage.CountyLanguageId;
                result.Name = countyLanguage.Name;
            }

            if (entity.Country != null)
            {
                result.CountryId = entity.CountryId;
                result.CountryName = entity.Country.CountryLanguages
                                           ?.FirstOrDefault(cl => cl.Language == _userInfo.CurrentLanguage)
                                           ?.Name;
            }
            
            return result;
        }

        public CountyVM Get(Guid id)
        {
            var entity = _countyDal.Get(id);
            var result = ToCountyVM(entity);
            return result;
        }

        public bool Add(CountyVM countyVm)
        {
            var entity = new County();
            entity = ToCountyInsertEntity(countyVm);
            return _countyDal.Add(entity);
        }

        private County ToCountyInsertEntity(CountyVM countyVm)
        {
            County entity;
            entity = new County();
            entity.CountyId = Guid.NewGuid();

            entity.CountyLanguages = new List<CountyLanguage>
                                      {
                                          new CountyLanguage
                                          {
                                              CountyLanguageId = Guid.NewGuid(),
                                              Language         = _userInfo.CurrentLanguage,
                                              Name             = countyVm.Name,
                                          }
                                      };

            entity.CountryId = countyVm.CountryId ;

            return entity;
        }

        public bool Update(CountyVM countyVm)
        {
            var result = new County
            {
                CountyId = countyVm.Id,
                CountyLanguages = new List<CountyLanguage>()
            };

            var item = new CountyLanguage();
            item.CountyId = countyVm.Id;
            item.Name = countyVm.Name;
            item.Language = countyVm.Language ?? _userInfo.CurrentLanguage;
            item.CountyLanguageId = countyVm.LanguageId ?? Guid.NewGuid();
            result.CountyLanguages.Add(item);

            result.CountryId = countyVm.CountryId;

            return _countyDal.Update(result);
        }

        public bool Del(Guid id)
        {
            return _countyDal.Delete(id);
        }

        public IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames(string currentLanguage)
        {
            var entities = _countryDal.GetIdAndCurrentLanguageNames(currentLanguage);
            return entities;
        }
    }
}