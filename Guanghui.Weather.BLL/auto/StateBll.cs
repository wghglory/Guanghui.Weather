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

    public partial class StateBll
    { 
    
        public IEnumerable<State> GetPagedDataByRowNum(int minrownum,int maxrownum)
		{
			return new StateDal().GetPagedDataByRowNum(minrownum,maxrownum);
		}
        
        public IEnumerable<State> GetPagedData(int pageSize,int pageIndex)
		{
			return new StateDal().GetPagedData(pageSize,pageIndex);
		}
		
		public IEnumerable<State> GetAll()
		{
			return new StateDal().GetAll();
		}
        
        public IEnumerable<State> Get()
		{
			return new StateDal().Get();
		}
        
       
        public IEnumerable<State> QueryList(int index, int size, object wheres, string orderField, bool isDesc = true)
        {
            return new StateDal().QueryList(index, size, wheres, orderField, isDesc);
        }
       

        public State QuerySingle(object wheres)
        {
            return new StateDal().QuerySingle(wheres);
        }
       
        public State QuerySingle(int stateId)
        {
            return new StateDal().QuerySingle(stateId);
        }
        
        public int QueryCount(object wheres)
        {
            return new StateDal().QueryCount(wheres);
        }
       
        
        public State GetByStateId(int stateId)
        {
            return new StateDal().GetByStateId(stateId);
        }
        public State Add(State state)
        {
            return new StateDal().Add(state);
        }

		public int Update(State state)
        {
            return new StateDal().Update(state);
        }
    
        public int DeleteByStateId(int stateId)
        {
            return new StateDal().DeleteByStateId(stateId);
        }
       
        
		public int GetTotalCount()
		{
			return new StateDal().GetTotalCount();
		}
		
		
    }
}