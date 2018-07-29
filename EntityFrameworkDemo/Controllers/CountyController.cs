using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkDemo.Controllers
{
    public class CountyController : Controller
    {
        private readonly ICountyBLL                _bll;
        private readonly ILogger<CountyController> _logAdapter;
        private readonly UserInfo                  _userInfo;

        public CountyController(ICountyBLL                bll,
                                ILogger<CountyController> logAdapter,
                                UserInfo                  userInfo)
        {
            _userInfo   = userInfo;
            _bll        = bll;
            _logAdapter = logAdapter;
        }

        public ActionResult Index()
        {
            var data = _bll.Get();
            return View(data);
        }

        public ActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var county = _bll.Get(id);
            if (county == null)
                return HttpNotFound();

            return View(county);
        }

        public ActionResult Create()
        {
            ViewBag.CountryId = GetCountryList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountyVM countyVm)
        {
            if (ModelState.IsValid)
            {
                _bll.Add(countyVm);
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = GetCountryList();
            return View(countyVm);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var county = _bll.Get(id);
            if (county == null)
                return HttpNotFound();

            ViewBag.CountryId = GetCountryList();
            return View(county);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountyVM county)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Update(county);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.CountryId = GetCountryList();
                    return View(county);
                }
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = GetCountryList();
            return View(county);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var county = _bll.Get(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _bll.Del(id);
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetCountryList()
        {
            var result = _bll.GetIdAndCurrentLanguageNames(_userInfo.CurrentLanguage)
                             .Select(cl=>new SelectListItem
                                         {
                                             Value = cl.CountryId.ToString(),
                                             Text  = cl.Name
                                         });
            return result;
        }
    }
}
