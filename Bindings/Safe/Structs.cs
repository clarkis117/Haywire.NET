﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Safe
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct HaywireString
	{
		public byte* value;
		public uint length;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct configuration
	{
		public byte* http_listen_address;
		public uint http_listen_port;
		public uint thread_count;
		public byte* parser;
		public bool tcp_nodelay;
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
		public IntPtr headers;
		public HaywireString* body;
		public uint body_length;
	}
}
