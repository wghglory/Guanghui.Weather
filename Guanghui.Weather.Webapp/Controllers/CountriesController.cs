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
    public class CountriesController : ApiController
    {
        readonly CountryBll _countryBll = new CountryBll();

        public IEnumerable<Country> Get()
        {
            return _countryBll.GetCountries();
        }

        public Country Get(int id)
        {
            return _countryBll.GetByCountryId(id);
        }
    }
}
