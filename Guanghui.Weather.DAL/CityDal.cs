using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.DAL
{
    public partial class CityDal
    {
        public IEnumerable<City> GetAllCities()
        {
            var list = new List<City>();
            string sql = @"SELECT Ci.CityId,Ci.CityName,Ci.CountryId,Co.CountryName,Ci.Temperature
                           FROM Cities Ci
                           JOIN Countries Co
                           on Ci.CountryId=Co.CountryId
                           order by Ci.CountryId";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    //list.Add(SqlHelper.MapEntity<City>(reader));
                    list.Add(new City()
                    {
                        CityId = (int)reader["CityId"],
                        CityName = reader["cityname"].ToString(),
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["countryname"].ToString(),
                        Temperature = (decimal)reader["temperature"]
                    });
                }
            }
            return list;
        }

        public IEnumerable<object> GetCitiesByTemperature(decimal temperature1, decimal temperature2)
        {
            var list = new List<City>();
            string sql = @"SELECT Ci.CityId,
                          Ci.CityName ,   
                          Ci.CountryId,
		                  Ci.Temperature,
		                  Co.CountryName
                          FROM [dbo].[Cities] Ci
                          join Countries Co
                          on Ci.CountryId=Co.CountryId
                          where Ci.[Temperature] between @t1 and @t2
                          order by Ci.countryId";

            SqlParameter[] pms =
            {
                new SqlParameter("@t1", temperature1),
                new SqlParameter("@t2", temperature2)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, pms))
            {
                while (reader.Read())
                {
                    //list.Add(SqlHelper.MapEntity<City>(reader));
                    list.Add(new City()
                    {
                        CityId = (int)reader["CityId"],
                        CityName = reader["cityname"].ToString(),
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["countryname"].ToString(),
                        Temperature = (decimal)reader["temperature"]
                    });
                }
            }
            var countries = list.GroupBy(x => x.CountryName).Select(x => new { Count = x.Count(), CountryName = x.Key });
            yield return new { Countries = countries, Cities = list };
        }

        public City GetById(int cityId)
        {
            string sql = @"SELECT C.CityId,C.CityName,C.CountryId,C.Temperature,Co.CountryName 
                            FROM Cities C
                            JOIN Countries Co
                            on C.CountryId=Co.CountryId
                            WHERE CityId = @CityId";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@CityId", cityId)))
            {
                if (reader.Read())
                {
                    return new City()
                    {
                        CityId = (int) reader["CityId"],
                        CityName = reader["cityname"].ToString(),
                        CountryId = (int) reader["CountryId"],
                        CountryName = reader["countryname"].ToString(),
                        Temperature = (decimal) reader["temperature"]
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
