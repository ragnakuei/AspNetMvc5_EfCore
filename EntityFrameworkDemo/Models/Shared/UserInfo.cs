using System.Threading;
using System.Web;
using EntityFrameworkDemo.Helpers;

namespace EntityFrameworkDemo.Models.Shared
{
    public class UserInfo
    {
        private readonly HttpContext _current;
        private readonly string _defaultLanguage;
        private readonly string _currentLanguage;

        public UserInfo(HttpContext current)
        {
            _current = current;

            _currentLanguage = current?.Request.Cookies["_culture"]?.Value
                                ?? Thread.CurrentThread.CurrentUICulture.ToString();
            _defaultLanguage = CultureHelper.GetDefaultCulture();
        }

        public string CurrentLanguage => _currentLanguage;

        public string DefaultLanguage => _defaultLanguage;
    }
}