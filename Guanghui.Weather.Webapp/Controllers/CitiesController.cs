using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Guanghui.Weather.BLL;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.Webapp.Controllers
{
    public class CitiesController : ApiController
    {
        CityBll _cityBll = new CityBll();

        public IEnumerable<City> Get()
        {
            return _cityBll.GetAllCities();
        }

        //http://localhost:7086/api/cities?t1=290&t2=292
        public IEnumerable<object> Get(decimal t1, decimal t2)
        {
            return _cityBll.GetCitiesByTemperature(t1, t2);
        }

        public City Get(int id)
        {
            return _cityBll.GetById(id);
        }

        public int Put(int id, City city)
        {
            return _cityBll.Update(city);
        }
    }
}
