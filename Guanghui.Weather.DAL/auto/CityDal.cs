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
	public partial class CityDal
	{
        
        public IEnumerable<City> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			var list = new List<City>();
			string sql = "SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature from(SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature,row_number() over(order by CityId) rownum FROM Cities) t where rownum>=@minrownum and rownum<=@maxrownum";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@minrownum",minrownum), new SqlParameter("@maxrownum",maxrownum)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<City> GetPagedData(int pageSize,int pageIndex)
		{
			var list = new List<City>();
			string sql = "SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature from(SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature,row_number() over(order by CityId) rownum FROM Cities) t where rownum between ((@pageIndex-1)*@pageSize)+1 and (@pageIndex*@pageSize)";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@pageSize",pageSize), new SqlParameter("@pageIndex",pageIndex)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
		
		public IEnumerable<City> GetAll()
		{
			var list = new List<City>();
			string sql = "SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature FROM Cities";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<City> Get()
		{
			string sql = "SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature FROM Cities";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					yield return SqlHelper.MapEntity<City>(reader);
				}				
			}
		}
        
        
        public IEnumerable<City> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
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
            string sql = SqlHelper.GenerateQuerySql("City", new[] { "CityId","CityName","StateId","CountryId","Latitude","Longitude","Temperature" }, index, size, whereBuilder.ToString(), orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, parameters.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        yield return SqlHelper.MapEntity<City>(reader);
                    }
                }
            }
        }
        
        public City QuerySingle(object wheres)
        {
            var list = QueryList(1, 1, wheres, null);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
       
        public City QuerySingle(int cityId)
        {
            const string sql = "SELECT TOP 1 CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature FROM Cities WHERE CityId = @CityId";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@CityId", cityId)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return SqlHelper.MapEntity<City>(reader);
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
            string sql = SqlHelper.GenerateQuerySql("City", new[] { "COUNT(1)" }, whereBuilder.ToString(), string.Empty);
            object res = SqlHelper.ExecuteScalar(sql, parameters.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        
        public City GetByCityId(int cityId)
        {
            string sql = "SELECT CityId,CityName,StateId,CountryId,Latitude,Longitude,Temperature FROM Cities WHERE CityId = @CityId";
            using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@CityId", cityId)))
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
		
        public City Add(City city)
		{
			string sql ="INSERT INTO Cities (CityName, StateId, CountryId, Latitude, Longitude, Temperature)  output inserted.CityId VALUES (@CityName, @StateId, @CountryId, @Latitude, @Longitude, @Temperature)";
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CityName", ToDBValue(city.CityName)),
				new SqlParameter("@StateId", ToDBValue(city.StateId)),
				new SqlParameter("@CountryId", ToDBValue(city.CountryId)),
				new SqlParameter("@Latitude", ToDBValue(city.Latitude)),
				new SqlParameter("@Longitude", ToDBValue(city.Longitude)),
				new SqlParameter("@Temperature", ToDBValue(city.Temperature)),
			};
					
			int newId = (int)SqlHelper.ExecuteScalar(sql, para);
			return GetByCityId(newId);
		}

							
        public int Update(City city)
        {
            string sql = "UPDATE Cities SET CityName=@CityName,StateId=@StateId,CountryId=@CountryId,Latitude=@Latitude,Longitude=@Longitude,Temperature=@Temperature WHERE CityId = @CityId"; 
            
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CityId", city.CityId)
					,new SqlParameter("@CityName", ToDBValue(city.CityName))
					,new SqlParameter("@StateId", ToDBValue(city.StateId))
					,new SqlParameter("@CountryId", ToDBValue(city.CountryId))
					,new SqlParameter("@Latitude", ToDBValue(city.Latitude))
					,new SqlParameter("@Longitude", ToDBValue(city.Longitude))
					,new SqlParameter("@Temperature", ToDBValue(city.Temperature))
			};

			return SqlHelper.ExecuteNonQuery(sql, para);
        }		
		
        public int DeleteByCityId(int cityId)
		{
            string sql = "DELETE Cities WHERE CityId = @CityId";

            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@CityId", cityId)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, para);
		}
        
        
		public City ToModel(SqlDataReader reader)
		{
			City city = new City();

			city.CityId = (int)ToModelValue(reader,"CityId");
			city.CityName = (string)ToModelValue(reader,"CityName");
			city.StateId = (int)ToModelValue(reader,"StateId");
			city.CountryId = (int)ToModelValue(reader,"CountryId");
			city.Latitude = (double)ToModelValue(reader,"Latitude");
			city.Longitude = (double)ToModelValue(reader,"Longitude");
			city.Temperature = (decimal)ToModelValue(reader,"Temperature");
			return city;
		}
		
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Cities";
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