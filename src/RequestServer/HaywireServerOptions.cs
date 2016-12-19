using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaywireNet.RequestServer
{
    public class HaywireServerOptions
    {
		/// <summary>
		/// should server head be added to each request
		/// </summary>
		public bool AddServerHeader { get; set; }

		public IServiceProvider ApplicationServices { get; set; }

		public uint MaxRequestBufferSize { get; set; }

		public uint ThreadCount { get; set; }
	}
}
