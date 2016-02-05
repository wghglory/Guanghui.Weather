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
	public partial class StateDal
	{
        
        public IEnumerable<State> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			var list = new List<State>();
			string sql = "SELECT StateId,StateName,CountryId,Latitude,Longitude from(SELECT StateId,StateName,CountryId,Latitude,Longitude,row_number() over(order by StateId) rownum FROM States) t where rownum>=@minrownum and rownum<=@maxrownum";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@minrownum",minrownum), new SqlParameter("@maxrownum",maxrownum)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<State> GetPagedData(int pageSize,int pageIndex)
		{
			var list = new List<State>();
			string sql = "SELECT StateId,StateName,CountryId,Latitude,Longitude from(SELECT StateId,StateName,CountryId,Latitude,Longitude,row_number() over(order by StateId) rownum FROM States) t where rownum between ((@pageIndex-1)*@pageSize)+1 and (@pageIndex*@pageSize)";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@pageSize",pageSize), new SqlParameter("@pageIndex",pageIndex)))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
		
		public IEnumerable<State> GetAll()
		{
			var list = new List<State>();
			string sql = "SELECT StateId,StateName,CountryId,Latitude,Longitude FROM States";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					list.Add(ToModel(reader));
				}				
			}
			return list;
		}
        
        public IEnumerable<State> Get()
		{
			string sql = "SELECT StateId,StateName,CountryId,Latitude,Longitude FROM States";
			using(SqlDataReader reader = SqlHelper.ExecuteReader(sql))
			{
				while(reader.Read())
				{
					yield return SqlHelper.MapEntity<State>(reader);
				}				
			}
		}
        
        
        public IEnumerable<State> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
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
            string sql = SqlHelper.GenerateQuerySql("State", new[] { "StateId","StateName","CountryId","Latitude","Longitude" }, index, size, whereBuilder.ToString(), orderField, isDesc);
            using (var reader = SqlHelper.ExecuteReader(sql, parameters.ToArray()))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        yield return SqlHelper.MapEntity<State>(reader);
                    }
                }
            }
        }
        
        public State QuerySingle(object wheres)
        {
            var list = QueryList(1, 1, wheres, null);
            return list != null && list.Any() ? list.FirstOrDefault() : null;
        }
       
        public State QuerySingle(int stateId)
        {
            const string sql = "SELECT TOP 1 StateId,StateName,CountryId,Latitude,Longitude FROM States WHERE StateId = @StateId";
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@StateId", stateId)))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return SqlHelper.MapEntity<State>(reader);
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
            string sql = SqlHelper.GenerateQuerySql("State", new[] { "COUNT(1)" }, whereBuilder.ToString(), string.Empty);
            object res = SqlHelper.ExecuteScalar(sql, parameters.ToArray());
            return res == null ? 0 : Convert.ToInt32(res);
        }
        
        public State GetByStateId(int stateId)
        {
            string sql = "SELECT StateId,StateName,CountryId,Latitude,Longitude FROM States WHERE StateId = @StateId";
            using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@StateId", stateId)))
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
		
        public State Add(State state)
		{
			string sql ="INSERT INTO States (StateName, CountryId, Latitude, Longitude)  output inserted.StateId VALUES (@StateName, @CountryId, @Latitude, @Longitude)";
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@StateName", ToDBValue(state.StateName)),
				new SqlParameter("@CountryId", ToDBValue(state.CountryId)),
				new SqlParameter("@Latitude", ToDBValue(state.Latitude)),
				new SqlParameter("@Longitude", ToDBValue(state.Longitude)),
			};
					
			int newId = (int)SqlHelper.ExecuteScalar(sql, para);
			return GetByStateId(newId);
		}

							
        public int Update(State state)
        {
            string sql = "UPDATE States SET StateName=@StateName,CountryId=@CountryId,Latitude=@Latitude,Longitude=@Longitude WHERE StateId = @StateId"; 
            
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@StateId", state.StateId)
					,new SqlParameter("@StateName", ToDBValue(state.StateName))
					,new SqlParameter("@CountryId", ToDBValue(state.CountryId))
					,new SqlParameter("@Latitude", ToDBValue(state.Latitude))
					,new SqlParameter("@Longitude", ToDBValue(state.Longitude))
			};

			return SqlHelper.ExecuteNonQuery(sql, para);
        }		
		
        public int DeleteByStateId(int stateId)
		{
            string sql = "DELETE States WHERE StateId = @StateId";

            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@StateId", stateId)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, para);
		}
        
        
		public State ToModel(SqlDataReader reader)
		{
			State state = new State();

			state.StateId = (int)ToModelValue(reader,"StateId");
			state.StateName = (string)ToModelValue(reader,"StateName");
			state.CountryId = (int)ToModelValue(reader,"CountryId");
			state.Latitude = (double)ToModelValue(reader,"Latitude");
			state.Longitude = (double)ToModelValue(reader,"Longitude");
			return state;
		}
		
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM States";
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