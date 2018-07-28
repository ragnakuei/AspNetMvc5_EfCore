using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.IBLL
{
    public interface ICountyBLL
    {
        List<CountyVM>               Get();
        CountyVM                     Get(Guid        id);
        bool                         Add(CountyVM    countyVm);
        bool                         Update(CountyVM countyVm);
        bool                         Del(Guid        id);
        IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames(string currentLanguage);
    }
}