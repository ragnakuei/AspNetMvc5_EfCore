﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkDemo.BLL;
using EntityFrameworkDemo.Controllers;
using EntityFrameworkDemo.DAL;
using EntityFrameworkDemo.EF;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.IDAL;
using EntityFrameworkDemo.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace EntityFrameworkDemo.DI
{
internal class DependencyInjectionResolver : IDependencyResolver
{
    private static readonly ServiceProvider _provider;

    static DependencyInjectionResolver()
    {
        var service = new ServiceCollection();

        service.AddTransient<HomeController>();
        service.AddTransient<CountryController>();
        service.AddTransient<CountyController>();
        service.AddTransient<CvController>();
        service.AddTransient<ICountryBLL, CountryBLL>();
        service.AddTransient<ICountyBLL, CountyBLL>();
        service.AddTransient<ICvBLL, CvBLL>();
        service.AddTransient<ICountryDAL, CountryDAL>();
        service.AddTransient<ICountyDAL, CountyDAL>();
        service.AddTransient<ICvDAL, CvDAL>();
        service.AddTransient<UserInfo, UserInfo>(s => new UserInfo(HttpContext.Current));

        service.AddLogging();       // Support ILogger<T>
        service.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        service.AddTransient<ILoggerFactory>(s =>
                                             {
                                                 var loggerFactory = new LoggerFactory();
                                                 loggerFactory.AddNLog();
                                                 return loggerFactory;
                                             });
        service.AddScoped(s =>
                               {
                                   var optionBuilder = new DbContextOptionsBuilder<DemoDbContext>();
                                   optionBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DemoDbContext"].ConnectionString);
                                   optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                                   var logFactory = _provider.GetService<ILoggerFactory>();
                                   optionBuilder.UseLoggerFactory(logFactory);
                                   return optionBuilder.Options;
                               });
        service.AddScoped<DemoDbContext, DemoDbContext>();

        _provider = service.BuildServiceProvider();
    }

    public object GetService(Type serviceType)
    {
        return _provider.GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return _provider.GetServices(serviceType);
    }
}
}