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
		byte* value;
		uint length;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Configuration
	{
		byte* http_listen_address;
		uint http_listen_port;
		uint htread_count;
		byte* parser;
		bool tcp_nodelay;
		uint listen_backlog;
		uint max_request_size;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct HttpRequest
	{
		ushort http_major;
		ushort http_minor;
		byte method;
		int keep_alive;
		HaywireString* url;
		void* headers;
		HaywireString* body;
		uint body_length;
		public enum state { OK, SIZE_EXCEEDED, BAD_REQUEST, INTERNAL_ERROR };
	}
}
