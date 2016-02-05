
//============================================================
//author:Guanghui Wang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Guanghui.Weather.Model
{	
	[Serializable()]
	public partial class State
	{	
		public int StateId { get; set; }
		public string StateName { get; set; }
		public int CountryId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
