using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.IDAL
{
    public interface ICountryDAL
    {
        IEnumerable<Country>         Get();
        bool                         Add(Country    country);
        Country                      Get(Guid       id);
        bool                         Update(Country entity);
        bool                         Delete(Guid    id);
        IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames(string currentLanguage);
    }
}