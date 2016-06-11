using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.MVC.DataViewModels;
using Weather.Model.Services;

namespace WeatherApp.MVC.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IWeatherService _weatherService;

        public ForecastController()
            :this(new WeatherService())
        {
            //Empty
        }

        public ForecastController(WeatherService weatherService)
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