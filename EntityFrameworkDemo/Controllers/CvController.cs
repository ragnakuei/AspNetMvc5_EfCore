using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.Log;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;

namespace EntityFrameworkDemo.Controllers
{
    public class CvController : Controller
    {
        private readonly ICvBLL     _cvBLL;
        private readonly LogAdapter _logAdapter;
        private readonly UserInfo   _userInfo;

        public CvController(ICvBLL     cvBLL,
                            LogAdapter logAdapter,
                            UserInfo   userInfo)
        {
            _userInfo   = userInfo;
            _cvBLL      = cvBLL;
            _logAdapter = logAdapter;
            _logAdapter.Initial(GetType().Name);
        }

        public ActionResult Index()
        {
            var data = _cvBLL.Get();
            ViewBag.CountryDict = GetCountryDict();
            ViewBag.CountyDict  = GetCountyDict();
            return View(data);
        }

        public ActionResult Create()
        {
            var vm = new CompCvVM
                     {
                         Certificates         = Enumerable.Repeat(new CompCvCertificateVM(), 2).ToList(),
                         Educations           = Enumerable.Repeat(new CompCvEducationVM(), 2).ToList(),
                         LanguageRequirements = Enumerable.Repeat(new CompCvLanguageRequirementVM(), 2).ToList()
                     };
            ViewBag.Language = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompCvVM cvVm)
        {
            if (ModelState.IsValid)
            {
                _cvBLL.Add(cvVm);
                return RedirectToAction("Index");
            }

            ViewBag.Language = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(cvVm);
        }

        public ActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var county = _cvBLL.Get(id);
            if (county == null)
                return HttpNotFound();

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(county);
        }


        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var compCv = _cvBLL.Get(id);
            if (compCv == null)
                return HttpNotFound();

            compCv.Certificates.Add(new CompCvCertificateVM());
            compCv.Educations.Add(new CompCvEducationVM());

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(compCv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompCvVM compCv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cvBLL.Update(compCv);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.Language  = GetLanguageOptions();
                    ViewBag.Listening = GetLanguageRequirementOptions();
                    ViewBag.CountryId = GetCountryOptions();
                    ViewBag.CountyId  = GetCountyOptions();
                    return View(compCv);
                }
                return RedirectToAction("Index");
            }

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(compCv);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var county = _cvBLL.Get(id);
            if (county == null)
            {
                return HttpNotFound();
            }

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(county);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _cvBLL.Del(id);
            return RedirectToAction("Index");
        }

        private Dictionary<Guid, string> GetCountryDict()
        {
            var result = _cvBLL.GetCountryIdNames(_userInfo.CurrentLanguage)
                               .ToDictionary(k => k.CountryId, v => v.Name);
            return result;
        }

        private Dictionary<Guid, string> GetCountyDict()
        {
            var result = _cvBLL.GetCountyIdNames(_userInfo.CurrentLanguage)
                               .ToDictionary(k => k.CountyId, v => v.Name);
            return result;
        }

        private IEnumerable<SelectListItem> GetCountryOptions()
        {
            var result = _cvBLL.GetCountryIdNames(_userInfo.CurrentLanguage)
                               .Select(c => new SelectListItem
                                            {
                                                Text  = c.Name,
                                                Value = c.CountryId.ToString(),
                                            });
            return result;
        }

        private IEnumerable<SelectListItem> GetCountyOptions()
        {
            var result = _cvBLL.GetCountyIdNames(_userInfo.CurrentLanguage)
                               .Select(c => new SelectListItem
                                            {
                                                Text  = c.Name,
                                                Value = c.CountyId.ToString(),
                                            });
            return result;
        }

        private object GetLanguageRequirementOptions()
        {
            return new List<SelectListItem>
                   {
                       new SelectListItem { Text  = "不會" , Value = "0", },
                       new SelectListItem { Text  = "略懂" , Value = "1", },
                       new SelectListItem { Text  = "中等" , Value = "2", },
                       new SelectListItem { Text  = "精通" , Value = "3", },
                   };
        }

        private object GetLanguageOptions()
        {
            return new List<SelectListItem>
                   {
                       new SelectListItem { Text = "中文" , Value = "0", },
                       new SelectListItem { Text = "英文" , Value = "1", },
                   };
        }
    }
}