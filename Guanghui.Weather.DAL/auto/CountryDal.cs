//============================================================
//author:Guanghui Wang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.DAL
{
	public partial class CountryDal
	{
        
        public IEnumerable<Country> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			var list = new List<Country>();
			string sql = "SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population from(SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population,row_number() over(order by CountryId) rownum FROM Countries) t where rownum>=@minrownum and rownum<=@maxrownum";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@minrownum",minrownum), new SqlParameter("@maxrownum",maxrownum)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<Country> GetPagedData(int pageSize,int pageIndex)
		{
			var list = new List<Country>();
			string sql = "SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population from(SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population,row_number() over(order by CountryId) rownum FROM Countries) t where rownum between ((@pageIndex-1)*@pageSize)+1 and (@pageIndex*@pageSize)";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@pageSize",pageSize), new SqlParameter("@pageIndex",pageIndex)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
		
		public IEnumerable<Country> GetAll()
		{
			var list = new List<Country>();
			string sql = "SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population FROM Countries";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<Country> Get()
		{
			string sql = "SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population FROM Countries";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					yield return SqlHelper.MapEntity<Country>(reader);
				}				
			}
		}
        
        
        public IEnumerable<Country> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            var parameters = new List<SqlParameter>();
            var whereBuilder = new System.Text.StringBuilder();
            if (wheres != null)
            {
                var props = wheres.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name.Equals("__o", StringComparison.InvariantCultureIgnoreCase))
                    {
                        whereBuilder.AppendFormat(" {0} ", prop.GetValue(wheres, null).ToString());
                    }
                    else
                    {
                        string val = prop.GetValue(wheres, null).ToString();
                        whereBuilder.AppendFormat(" [{0}] = @{0} ", prop.Name);
                        parameters.Add(new SqlParameter("@" + prop.Name, val));
                    }
                }
            }
            string sql = SqlHelper.GenerateQuerySql("Country", new[] { "CountryId","CountryName","LocalName","WebCode","Region","Continent","Latitude","Longitude","SurfaceArea","Population" }, index, size, whereBuilder.ToString(), orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, parameters.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        yield return SqlHelper.MapEntity<Country>(reader);
                    }
                }
            }
        }
        
        public Country QuerySingle(object wheres)
        {
            var list = QueryList(1, 1, wheres, null);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
       
        public Country QuerySingle(int countryId)
        {
            const string sql = "SELECT TOP 1 CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population FROM Countries WHERE CountryId = @CountryId";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@CountryId", countryId)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return SqlHelper.MapEntity<Country>(reader);
                }
                else
                {
                    return null;
                }
            }
        }
       
        public int QueryCount(object wheres)
        {
            var parameters = new List<SqlParameter>();
            var whereBuilder = new System.Text.StringBuilder();
            if (wheres != null)
            {
                var props = wheres.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name.Equals("__o", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 操作符
                        whereBuilder.AppendFormat(" {0} ", prop.GetValue(wheres, null).ToString());
                    }
                    else
                    {
                        string val = prop.GetValue(wheres, null).ToString();
                        whereBuilder.AppendFormat(" [{0}] = @{0} ", prop.Name);
                        parameters.Add(new SqlParameter("@" + prop.Name, val));
                    }
                }
            }
            string sql = SqlHelper.GenerateQuerySql("Country", new[] { "COUNT(1)" }, whereBuilder.ToString(), string.Empty);
            object res = SqlHelper.ExecuteScalar(sql, parameters.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        
        public Country GetByCountryId(int countryId)
        {
            string sql = "SELECT CountryId,CountryName,LocalName,WebCode,Region,Continent,Latitude,Longitude,SurfaceArea,Population FROM Countries WHERE CountryId = @CountryId";
            using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@CountryId", countryId)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
       		}
        }
		
        public Country Add(Country country)
		{
			string sql ="INSERT INTO Countries (CountryName, LocalName, WebCode, Region, Continent, Latitude, Longitude, SurfaceArea, Population)  output inserted.CountryId VALUES (@CountryName, @LocalName, @WebCode, @Region, @Continent, @Latitude, @Longitude, @SurfaceArea, @Population)";
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CountryName", ToDBValue(country.CountryName)),
				new SqlParameter("@LocalName", ToDBValue(country.LocalName)),
				new SqlParameter("@WebCode", ToDBValue(country.WebCode)),
				new SqlParameter("@Region", ToDBValue(country.Region)),
				new SqlParameter("@Continent", ToDBValue(country.Continent)),
				new SqlParameter("@Latitude", ToDBValue(country.Latitude)),
				new SqlParameter("@Longitude", ToDBValue(country.Longitude)),
				new SqlParameter("@SurfaceArea", ToDBValue(country.SurfaceArea)),
				new SqlParameter("@Population", ToDBValue(country.Population)),
			};
					
			int newId = (int)SqlHelper.ExecuteScalar(sql, para);
			return GetByCountryId(newId);
		}

							
        public int Update(Country country)
        {
            string sql = "UPDATE Countries SET CountryName=@CountryName,LocalName=@LocalName,WebCode=@WebCode,Region=@Region,Continent=@Continent,Latitude=@Latitude,Longitude=@Longitude,SurfaceArea=@SurfaceArea,Population=@Population WHERE CountryId = @CountryId"; 
            
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CountryId", country.CountryId)
					,new SqlParameter("@CountryName", ToDBValue(country.CountryName))
					,new SqlParameter("@LocalName", ToDBValue(country.LocalName))
					,new SqlParameter("@WebCode", ToDBValue(country.WebCode))
					,new SqlParameter("@Region", ToDBValue(country.Region))
					,new SqlParameter("@Continent", ToDBValue(country.Continent))
					,new SqlParameter("@Latitude", ToDBValue(country.Latitude))
					,new SqlParameter("@Longitude", ToDBValue(country.Longitude))
					,new SqlParameter("@SurfaceArea", ToDBValue(country.SurfaceArea))
					,new SqlParameter("@Population", ToDBValue(country.Population))
			};

			return SqlHelper.ExecuteNonQuery(sql, para);
        }		
		
        public int DeleteByCountryId(int countryId)
		{
            string sql = "DELETE Countries WHERE CountryId = @CountryId";

            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CountryId", countryId)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, para);
		}
        
        
		public Country ToModel(SqlDataReader reader)
		{
			Country country = new Country();

			country.CountryId = (int)ToModelValue(reader,"CountryId");
			country.CountryName = (string)ToModelValue(reader,"CountryName");
			country.LocalName = (string)ToModelValue(reader,"LocalName");
			country.WebCode = (string)ToModelValue(reader,"WebCode");
			country.Region = (string)ToModelValue(reader,"Region");
			country.Continent = (string)ToModelValue(reader,"Continent");
			country.Latitude = (double)ToModelValue(reader,"Latitude");
			country.Longitude = (double)ToModelValue(reader,"Longitude");
			country.SurfaceArea = (double)ToModelValue(reader,"SurfaceArea");
			country.Population = (long)ToModelValue(reader,"Population");
			return country;
		}
		
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Countries";
			return (int)SqlHelper.ExecuteScalar(sql);
		}
		

		
		public object ToDBValue(object value)
		{
			if(value == null)
			{
				return DBNull.Value;
			}
			else
			{
				return value;
			}
		}
		
		public object ToModelValue(SqlDataReader reader,string columnName)
		{
			if(reader.IsDBNull(reader.GetOrdinal(columnName)))
			{
				return null;
			}
			else
			{
				return reader[columnName];
			}
		}
	}
}