using HaywireNet.Bindings.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct HttpRequest
	{
		public enum State { OK = 0, SIZE_EXCEEDED, BAD_REQUEST, INTERNAL_ERROR };

		public ushort http_major;
		public ushort http_minor;
		public HttpMethod method;
		public int keep_alive;

		/// <summary>
		/// ptr to haywire string
		/// </summary>
		private IntPtr url;

		public HaywireString Url
		{
			get { return Marshal.PtrToStructure<HaywireString>(url); }
		}

		/// <summary>
		/// original void*
		/// </summary>
		public IntPtr headers;

		/// <summary>
		/// ptr to haywire string
		/// </summary>
		public IntPtr body;

		public HaywireString Body
		{
			get { return Marshal.PtrToStructure<HaywireString>(body); }
		}

		public uint body_length;

		public State state;
	}
}
