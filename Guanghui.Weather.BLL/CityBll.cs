using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guanghui.Weather.DAL;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.BLL
{
    public partial class CityBll
    {
        public IEnumerable<City> GetAllCities()
        {
            return new CityDal().GetAllCities();
        }

        public IEnumerable<object> GetCitiesByTemperature(decimal temperature1, decimal temperature2)
        {
            return new CityDal().GetCitiesByTemperature(temperature1, temperature2);
        }

        public City GetById(int cityId)
        {
            return new CityDal().GetById(cityId);
        }
    }
}
