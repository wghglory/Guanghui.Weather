
//============================================================
//author:Guanghui Wang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Guanghui.Weather.Model
{	
	[Serializable()]
	public partial class City
	{	
		public int CityId { get; set; }
		public string CityName { get; set; }
		public int StateId { get; set; }
		public int CountryId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public decimal Temperature { get; set; }
	}
}
