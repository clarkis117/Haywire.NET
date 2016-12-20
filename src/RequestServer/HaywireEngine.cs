using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaywireNet.Bindings;
using static HaywireNet.Bindings.Haywire;

namespace HaywireNet.RequestServer
{
	public class HaywireEngine
	{
		//this should take the server
		public HaywireEngine()
		{

			//config haywire

			//
			_rootCb = GetRoot;

			//hack haywire routing
			NativeMethods.hw_http_add_route("/", _rootCb, IntPtr.Zero);
			NativeMethods.hw_http_add_route("/*", _rootCb, IntPtr.Zero);
		}

		private readonly http_request_callback _rootCb;

		private void GetRoot(ref HttpRequest request, IntPtr response, IntPtr user_data)
		{
			//create feature collection

			//parse request to IHttpRequest Feature

			//some waiting then send response
		}
	}
}
