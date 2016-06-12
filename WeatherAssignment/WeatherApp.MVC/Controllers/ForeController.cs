using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.MVC.DataViewModels;
using Weather.Model.Services;
using System.Net;
using Weather.Model.Repositories;
using Weather.Model;
using System.Data;

namespace WeatherApp.MVC.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IWeatherService _weatherService;

        private readonly IWeatherRepository _iweatherRepository;

        public ForecastController()
            :this(new WeatherService(), new WeatherRepository())
        {
            //Empty. Used for Dependency injection
        }

        public ForecastController(IWeatherService weatherService, IWeatherRepository iweatherRepository)
        {
            _weatherService = weatherService;
            _iweatherRepository = iweatherRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new WeahterIndexViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index
        (
            [Bind(Include = "GeonameSearch, countryCode, MaxRow, GeonameId")]
            WeahterIndexViewModel model
        )
        {
            
            if (ModelState.IsValid)
            {

                if (Session["geonames"] != null)
                {
                    model.Geonames = Session["geonames"] as IEnumerable<Weather.Model.Geoname>;
                    Session["geonames"] = null;
                    model.Geoname = model.Geonames.Where(g => g.geonameId == model.GeonameId).SingleOrDefault();


                    model.Forecasts = _weatherService.RefreshForecast
                                                      (
                                                          model.GeonameId,
                                                          model.Geoname.lat,
                                                          model.Geoname.lng
                                                      );
                }

                //State seems to be ok. But we have no geonames to test against!
                //We use a webservice and save a collection Geonames in session Session["geonames"]
                else
                {
                    model.Geonames = _weatherService.GetGeonames
                                                     (
                                                         model.GeonameSearch,
                                                         model.CountryCode,
                                                         int.Parse(model.MaxRow)
                                                     );
                    Session["geonames"] = model.Geonames;
                }
            }
            
            return View(model);
        }

        // GET: /Forecast/Edit/777
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Forecast forecast = _iweatherRepository.FindForecastById(id.Value);
            if (forecast == null)
            {
                return HttpNotFound();
            }

            return View(forecast);
        }

        // POST: /Forecast/Edit/777
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id)
        {
            var forecasttoUpdate = _iweatherRepository.FindForecastById(id);

            if (forecasttoUpdate == null)
            {
                return HttpNotFound();
            }

            //Fix Bind here to prevent overposting?
            //Version Mats. L.
            //if (TryUpdateModel(birthdaytoUpdate, String.Empty, new string[] { "Name", "Birthdate" }))
            //https://github.com/1dv409/kursmaterial/blob/master/Exempel/NextBirthday.VS2013/NextBirthday.4.CRUD/Controllers/BirthdayController.cs#L83
            if (TryUpdateModel(forecasttoUpdate))
            {
                try
                {
                    _iweatherRepository.UpdateForecast(forecasttoUpdate);
                    _iweatherRepository.Save();
                    TempData["success"] = "...now saved.";
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    TempData["error"] = "Failed to save the changes. Try again , and the problem persists, contact your system administrator.";
                }
            }

            return View(forecasttoUpdate);
        }

        // GET: /Forecast/Delete/777
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forecast = _iweatherRepository.FindForecastById(id.Value);
            if (forecast == null)
            {
                return HttpNotFound();
            }

            return View(forecast);
        }

        // POST: /Forecast/Delete/777
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var forecastToDelete = new Forecast { forecastId = id };
                _iweatherRepository.RemoveForecast(forecastToDelete);
                _iweatherRepository.Save();
                TempData["success"] = "Forecast was removed!";
            }
            catch (DataException)
            {
                TempData["error"] = "Failed that - remove the Forecast. Try again, and the problem persists, contact your system administrator .";
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _iweatherRepository.Dispose();
            base.Dispose(disposing);
        }

        // GET: /Forecast/Details/777
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forecast = _iweatherRepository.FindForecastById(id.Value);
            if (forecast == null)
            {
                return HttpNotFound();
            }

            return View(forecast);
        }

        //From default startup below....
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}