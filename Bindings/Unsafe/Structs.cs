using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Unsafe.Structs
{
	[StructLayout(LayoutKind.Sequential)]
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
		public byte* http_listen_address;
		public uint http_listen_port;
		public uint thread_count;
		public byte* parser;
		//[MarshalAs(UnmanagedType.I4)]
		public bool tcp_nodelay;
		//[MarshalAs(UnmanagedType.U4)]
		public uint listen_backlog;
		public uint max_request_size;
	}

	public enum state { OK = 0, SIZE_EXCEEDED, BAD_REQUEST, INTERNAL_ERROR };

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct HttpRequest
	{
		public ushort http_major;
		public ushort http_minor;
		public byte method;
		public int keep_alive;
		public HaywireString* url;
		public void* headers;
		public HaywireString* body;
		public uint body_length;
	}
}
