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
    public partial class CountryBll
    {
        public IEnumerable<Country> GetCountries()
        {
            return new CountryDal().GetCountries();
        }
    }
}
