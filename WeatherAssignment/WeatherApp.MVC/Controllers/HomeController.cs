using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.MVC.DataViewModels;
using Weather.Model.Services;

namespace WeatherApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        HomeController()
            :this(new WeatherService())
        {
            //Empty
        }

        HomeController(WeatherService weatherService)
        {
            _weatherService = weatherService;
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
                if(Session["geonames"] != null)
                {
                    model.Geonames = Session["geonames"] as IEnumerable<Weather.Model.Geoname>;
                    Session["geonames"] = null;
                    model.Geoname = model.Geonames.Where(g => g.geonameId == model.GeonameId).SingleOrDefault();


                    model.Forecasts = new WeatherService().
                    RefreshForecast
                    (
                        model.Geoname.geonameId,
                        model.Geoname.lat,
                        model.Geoname.lng
                    );
                }
                //State seems to be ok. But we have no geonames to test against!
                //We use a webservice band save a collection Geonames in session Session["geonames"]
                else
                {
                    model.Geonames = new Weather.Domain.Webservices.GeonamesWebservice().
                                    GetGeonames
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

        [HttpPost]
        public ActionResult GetForecastsByDay(int dayDifferanceFromToday)
        {
            //Ok Ajax request
            return Json("adf");
            //return PartialView("ForecastCollectionByDay");
        }


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