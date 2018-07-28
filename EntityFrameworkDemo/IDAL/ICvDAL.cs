using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.IDAL
{
    public interface ICvDAL
    {
        IEnumerable<CompCv>  Get();
        CompCv Get(Guid id);
        IEnumerable<CountryLanguage> GetCountryIdNames(string currentLanguage);
        IEnumerable<CountyLanguage> GetCountyIdNames(string currentLanguage);
        void Add(CompCv entity);
        bool Update(CompCv entity);
        bool Delete(Guid id);
    }
}