using System.Linq;
using System.Web.Mvc;
using EntityFrameworkDemo.Helpers;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserInfo _userInfo;

        public HomeController(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public ActionResult Index()
        {
            ViewBag.CurrentLanguage = _userInfo.CurrentLanguage;

            ViewData["LangSelectList"] = CultureHelper.GetAllImplementedCultures()
                                                      .Select(c =>
                                                              {
                                                                  var item = new SelectListItem();
                                                                  item.Text = c.Value;
                                                                  item.Value = c.Value;
                                                                  item.Selected = c.Value.ToLower() == _userInfo.CurrentLanguage.ToLower();
                                                                  return item;
                                                              });


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set(CultureVM cultureVm)
        {
            CultureHelper.SetCulture(HttpContext, cultureVm.Language);
            return RedirectToAction("Index");
        }

    }
}