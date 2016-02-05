using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.DAL
{
    public partial class CountryDal
    {
        public IEnumerable<Country> GetCountries()
        {
            var list = new List<Country>();
            string sql = @"SELECT Ci.countryid,Co.countryName,avg(Ci.Temperature) Temperature
                           FROM[GuanghuiWeather].[dbo].[Cities] Ci
                           join Countries Co
                           on Ci.CountryId = Co.CountryId
                           group by Ci.CountryId,Co.countryName
                           order by Ci.CountryId";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    list.Add(new Country()
                    {
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["countryname"].ToString(),
                        Temperature = Convert.ToDecimal(((decimal)reader["temperature"]).ToString("#.##"))
                    });
                }
            }
            return list;

        }
    }
}
