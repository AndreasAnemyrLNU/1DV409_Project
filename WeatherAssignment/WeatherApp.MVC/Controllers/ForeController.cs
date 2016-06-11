using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.MVC.DataViewModels;
using Weather.Model.Services;
using System.Net;
using Weather.Model.Repositories;

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


        /* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        // POST: /Birthday/Delete/42
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var studentToDelete = new Birthday { BirthdayId = id };
                _iweatherRepository.DeleteBirthday(studentToDelete);
                _iweatherRepository.Save();
                TempData["success"] = "Födelsedatumet togs bort.";
            }
            catch (DataException)
            {
                TempData["error"] = "Misslyckades att ta bort födelsedatumet. Försök igen, och kvarstår problemet kontakta systemadministratören.";
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _iweatherRepository.Dispose();
            base.Dispose(disposing);
        }

        */ 

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