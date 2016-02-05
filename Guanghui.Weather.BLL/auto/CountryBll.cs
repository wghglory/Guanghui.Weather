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

    public partial class CountryBll
    { 
    
        public IEnumerable<Country> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			return new CountryDal().GetPagedDataByRowNum(minrownum,maxrownum);
		}
        
        public IEnumerable<Country> GetPagedData(int pageSize,int pageIndex)
		{
			return new CountryDal().GetPagedData(pageSize,pageIndex);
		}
		
		public IEnumerable<Country> GetAll()
		{
			return new CountryDal().GetAll();
		}
        
        public IEnumerable<Country> Get()
		{
			return new CountryDal().Get();
		}
        
       
        public IEnumerable<Country> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            return new CountryDal().QueryList(index, size, wheres, orderField, isDesc);
        }
       

        public Country QuerySingle(object wheres)
        {
            return new CountryDal().QuerySingle(wheres);
        }
       
        public Country QuerySingle(int countryId)
        {
            return new CountryDal().QuerySingle(countryId);
        }
        
        public int QueryCount(object wheres)
        {
            return new CountryDal().QueryCount(wheres);
        }
       
        
        public Country GetByCountryId(int countryId)
        {
            return new CountryDal().GetByCountryId(countryId);
        }
        public Country Add(Country country)
        {
            return new CountryDal().Add(country);
        }

		public int Update(Country country)
        {
            return new CountryDal().Update(country);
        }
    
        public int DeleteByCountryId(int countryId)
        {
            return new CountryDal().DeleteByCountryId(countryId);
        }
       
        
		public int GetTotalCount()
		{
			return new CountryDal().GetTotalCount();
		}
		
		
    }
}