using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using EntityFrameworkDemo.Enums;

namespace EntityFrameworkDemo.Helpers
{
    public class CultureHelper
    {
        public class Culture
        {
            public int    Key   { get; set; }
            public string Value { get; set; }
        }

        public static string GetImplementedCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture)                                 == false
                && GetAllImplementedCultures().Count(x => x.Value.Equals(culture)) > 0)
                return culture;

            return GetDefaultCulture();
        }

        public static IEnumerable<Culture> GetAllImplementedCultures()
        {
            return Enum.GetValues(typeof(CultureEnum))
                       .Cast<CultureEnum>()
                       .Select(cl => new Culture
                                     {
                                         Key   = cl.ToIntValue(),
                                         Value = cl.GetDescription()
                                     });
        }

        public static void SetCulture(HttpContextBase httpContext, string language)
        {
            var culture       = GetImplementedCulture(language);
            var cultureCookie = httpContext.Request.Cookies["_culture"];
            if (cultureCookie == null)
            {
                cultureCookie = new HttpCookie("_culture")
                                {
                                    HttpOnly = true,
                                    Value    = culture,
                                    Expires  = DateTime.Now.AddMonths(2)
                                };
            }
            else
            {
                cultureCookie.Value = culture;
            }

            httpContext.Response.Cookies.Add(cultureCookie);

            var ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture   = ci;
        }

        public static string GetDefaultCulture()
        {
            return CultureEnum.en_US.GetDescription(); //預設為en-US 英文
        }
    }
}