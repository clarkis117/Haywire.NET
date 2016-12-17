using HaywireNet.Bindings.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Structs
{


	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct configuration
	{
		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		public string http_listen_address;
		public uint http_listen_port;
		public uint thread_count;

		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		public string balancer;
		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		public string parser;
		public bool tcp_nodelay;
		public uint listen_backlog;
		public uint max_request_size;
	}

	public enum state { OK = 0, SIZE_EXCEEDED, BAD_REQUEST, INTERNAL_ERROR };

	[StructLayout(LayoutKind.Sequential)]
	public struct HttpRequest
	{
		public ushort http_major;
		public ushort http_minor;
		public byte method;
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
		/// originally void pointer
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
	}
}
