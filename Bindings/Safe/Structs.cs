using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Safe
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public unsafe struct HaywireString
	{
		public byte* value;
		public uint length;

		public HaywireString(byte* ptrValue, int stringLength)
		{
			value = ptrValue;
			length = (uint)stringLength;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct configuration
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string http_listen_address;
		public uint http_listen_port;
		public uint thread_count;
		[MarshalAs(UnmanagedType.LPStr)]
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
		//Haywire String
		public IntPtr Url;
		public IntPtr headers;
		//HayWire String
		public IntPtr body;
		public uint body_length;
	}
}
