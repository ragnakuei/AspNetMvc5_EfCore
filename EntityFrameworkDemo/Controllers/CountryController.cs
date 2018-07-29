using System;
using System.Net;
using System.Web.Mvc;
using EntityFrameworkDemo.IBLL;
using EntityFrameworkDemo.Models.Shared;
using EntityFrameworkDemo.Models.ViewModel;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkDemo.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryBLL                _bll;
        private readonly ILogger<CountryController> _logAdapter;
        private readonly UserInfo                   _userInfo;

        public CountryController(ICountryBLL bll, ILogger<CountryController> logAdapter, UserInfo userInfo)
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

            var country = _bll.Get(id);
            if (country == null)
                return HttpNotFound();
            
            return View(country);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryVM countryVm)
        {
            if (ModelState.IsValid)
            {
                _bll.Add(countryVm);
                return RedirectToAction("Index");
            }

            return View(countryVm);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var country = _bll.Get(id);
            if (country == null)
                return HttpNotFound();

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryVM country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Update(country);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty,e.Message);
                    return View(country);
                }
                return RedirectToAction("Index");
            }
            return View(country);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var country = _bll.Get(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _bll.Del(id);
            return RedirectToAction("Index");
        }
    }
}
