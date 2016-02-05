
//============================================================
//author:Guanghui Wang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Guanghui.Weather.Model
{	
	[Serializable()]
	public partial class Country
	{	
		public int CountryId { get; set; }
		public string CountryName { get; set; }
		public string LocalName { get; set; }
		public string WebCode { get; set; }
		public string Region { get; set; }
		public string Continent { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double SurfaceArea { get; set; }
		public long Population { get; set; }
	}
}
