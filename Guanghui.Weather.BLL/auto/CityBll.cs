//============================================================
//author:Guanghui Wang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using Guanghui.Weather.DAL;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.BLL
{

    public partial class CityBll
    { 
    
        public IEnumerable<City> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			return new CityDal().GetPagedDataByRowNum(minrownum,maxrownum);
		}
        
        public IEnumerable<City> GetPagedData(int pageSize,int pageIndex)
		{
			return new CityDal().GetPagedData(pageSize,pageIndex);
		}
		
		public IEnumerable<City> GetAll()
		{
			return new CityDal().GetAll();
		}
        
        public IEnumerable<City> Get()
		{
			return new CityDal().Get();
		}
        
       
        public IEnumerable<City> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            return new CityDal().QueryList(index, size, wheres, orderField, isDesc);
        }
       

        public City QuerySingle(object wheres)
        {
            return new CityDal().QuerySingle(wheres);
        }
       
        public City QuerySingle(int cityId)
        {
            return new CityDal().QuerySingle(cityId);
        }
        
        public int QueryCount(object wheres)
        {
            return new CityDal().QueryCount(wheres);
        }
       
        
        public City GetByCityId(int cityId)
        {
            return new CityDal().GetByCityId(cityId);
        }
        public City Add(City city)
        {
            return new CityDal().Add(city);
        }

		public int Update(City city)
        {
            return new CityDal().Update(city);
        }
    
        public int DeleteByCityId(int cityId)
        {
            return new CityDal().DeleteByCityId(cityId);
        }
       
        
		public int GetTotalCount()
		{
			return new CityDal().GetTotalCount();
		}
		
		
    }
}