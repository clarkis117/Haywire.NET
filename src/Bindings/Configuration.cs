using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct Configuration
	{
		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		private string http_listen_address;

		public string HttpListenAddress
		{
			get { return http_listen_address; }
			set { http_listen_address = value; }
		}

		private uint http_listen_port;

		public uint HttpListenPort
		{
			get { return http_listen_port; }
			set { http_listen_port = value; }
		}

		private uint thread_count;

		public uint ThreadCount
		{
			get { return thread_count; }
			set { thread_count = value; }
		}

		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		private string balancer;

		public string Balancer
		{
			get { return balancer; }
			set { balancer = value; }
		}

		[MarshalAs(UnmanagedType.LPStr, ArraySubType = UnmanagedType.LPArray)]
		private string parser;

		public string Parser
		{
			get { return parser; }
			set { parser = value; }
		}


		[MarshalAs(UnmanagedType.Bool)]
		private bool tcp_nodelay;

		public bool TcpNoDelay
		{
			get { return tcp_nodelay; }
			set { tcp_nodelay = value; }
		}

		private uint listen_backlog;

		public uint ListenBackLog
		{
			get { return listen_backlog; }
			set
			{
				listen_backlog = value;
			}
		}

		private uint max_request_size;

		public uint MaxRequestSize {
			get { return max_request_size; }
			set { max_request_size = value;  }
		}

		public Configuration(
			string httpListenAddress = "localhost",
			uint httpListenPort = 8000,
			uint threadCount = 0,
			string balancer = "ipc",
			string parser = "http_parser",
			bool tcpNoDelay = false,
			uint listenBacklog = 0,
			uint maxRequestSize = 1048576
			)
		{
			http_listen_address = httpListenAddress;
			http_listen_port = httpListenPort;
			thread_count = threadCount;
			this.balancer = balancer;
			this.parser = parser;
			tcp_nodelay = tcpNoDelay;
			listen_backlog = listenBacklog;
			max_request_size = maxRequestSize;
		}
	}
}
