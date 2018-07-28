using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.IDAL;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemo.DAL
{
    public class CountryDAL : ICountryDAL, IDisposable
    {
        private readonly DemoDbContext _dbContext;
        private readonly LogAdapter    _logger;
        private readonly UserInfo      _userInfo;

        public CountryDAL(DemoDbContext dbContext,
                          LogAdapter    logger,
                          UserInfo      userInfo)
        {
            _dbContext = dbContext;
            _logger    = logger;
            _userInfo  = userInfo;
            _logger.Initial<CountryDAL>();
        }

        public IEnumerable<Country> Get()
        {
            // 使用 Left Join
            return _dbContext.Country
                             .Include(c => c.CountryLanguages);
        }

        public Country Get(Guid id)
        {
            // 分開查詢
            var country = _dbContext.Country
                                    .FirstOrDefault(c => c.CountryId == id);
            if (country == null)
                throw new Exception("查無資料");

            var countryLanguage = _dbContext.CountryLanguage
                                            .Where(l => l.CountryId   == id
                                                        && l.Language == _userInfo.CurrentLanguage);
            country.CountryLanguages = countryLanguage.ToList();
            return country;
        }

        public bool Add(Country country)
        {
            _dbContext.Country.Add(country);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Country updateEntity)
        {
            if (updateEntity == null)
                throw new Exception("Country 無對應資料可更新");

            var countryInDB = _dbContext.Country
                                        .Include(c=>c.CountryLanguages)
                                        .FirstOrDefault(c => c.CountryId == updateEntity.CountryId);
            if (countryInDB == null)
                throw new Exception("Country 無對應資料可更新");

            foreach (var countryLanguage in countryInDB.CountryLanguages)
            {
                countryLanguage.Name = updateEntity.CountryLanguages
                                                   .FirstOrDefault(cl => cl.CountryLanguageId == countryLanguage.CountryLanguageId)
                                                   ?.Name;
                _dbContext.Entry(countryLanguage).Property(c=>c.Name).IsModified = true;
            }
         
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {  
            var delCountry = _dbContext.Country
                                       .FirstOrDefault(c => c.CountryId == id);
            var delCountryLanguages = _dbContext.CountryLanguage
                                                .Where(c => c.CountryId == delCountry.CountryId);

            _dbContext.CountryLanguage
                      .RemoveRange(delCountryLanguages);

            _dbContext.Country
                      .Remove(delCountry);

            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames(string currentLanguage)
        {
            var result = _dbContext.CountryLanguage
                                   .Where(cl => cl.Language == currentLanguage)
                                   .Include(cl => cl.Country)
                                   .Select(cl=>new
                                               {
                                                   cl.CountryId,
                                                   cl.Name
                                               })
                                   .AsEnumerable()
                                   .Select(anon=>new CountryLanguage
                                                 {
                                                     CountryId = anon.CountryId,
                                                     Name = anon.Name
                                                 });
            return result;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}