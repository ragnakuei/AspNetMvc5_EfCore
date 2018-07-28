using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.IBLL
{
    public interface ICountryBLL
    {
        List<CountryVM> Get();
        CountryVM       Get(Guid         id);
        bool            Add(CountryVM    countryVm);
        bool            Update(CountryVM countryVm);
        bool            Del(Guid         id);
    }
}