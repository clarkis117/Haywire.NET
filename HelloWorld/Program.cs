using HaywireNet.Bindings.Extensions;
using HaywireNet.Bindings;
using HaywireNet.Bindings.Structs;
using System;
using System.Runtime.InteropServices;
using static HaywireNet.Bindings.Haywire;

namespace HelloWorld
{
	/// <summary>
	/// A simple hello world program based on the hello world sample in github.com/haywire/haywire
	/// </summary>
	public class Program
	{
		private const string RootRoute = "/";
		private const string PingRoute = "/ping";
		private const string DefaultAddress = "localhost"; //"192.168.1.103";
		private const string HttpOk = "200 OK";
		private const string ContentTypeName = "Content-Type";
		private const string ContentTypeValue = "text/html";
		private const string KeepAliveName = "Connection";
		private const string KeepAliveValue = "Keep-Alive";
		private const string Body = "Hello World";
		private const string UserData = "user_data";
		private const uint DefaultPort = 8800;
		private const string Parser = "http_parser";

		static HaywireString statusCode = new HaywireString(HttpOk);
		static HaywireString contenttypename = new HaywireString(ContentTypeName);
		static HaywireString contenttypevalue = new HaywireString(ContentTypeValue);
		static HaywireString body = new HaywireString(Body);
		static HaywireString keepalivename = new HaywireString(KeepAliveName);
		static HaywireString keepalivevalue = new HaywireString(KeepAliveValue);

		//root
		private static readonly http_request_callback _rootCallback = GetRoot;
		//private static readonly GCHandle delegateHandleRoot = GCHandle.Alloc(_rootCallback);
		//private static readonly IntPtr functionpointerRoot = Marshal.GetFunctionPointerForDelegate(_rootCallback);

		//res
		private static readonly http_response_complete_callback _responseRoot = ResponseComplete;
		//private static readonly GCHandle delgetateRes = GCHandle.Alloc(_responseRoot);
		//private static readonly IntPtr functionpointerres = Marshal.GetFunctionPointerForDelegate(_responseRoot);

		public static void Main(string[] args)
		{
			Console.WriteLine("2nd unsafe version");

			configuration config;

			config.http_listen_address = DefaultAddress;
			config.http_listen_port = DefaultPort;
			config.thread_count = 12;
			config.balancer = "ipc";
			config.parser = Parser;
			config.max_request_size = 1048576;
			config.tcp_nodelay = false;
			config.listen_backlog = 0;

			NativeMethods.hw_init_with_config(ref config);

			NativeMethods.hw_http_add_route(RootRoute, _rootCallback, IntPtr.Zero);

			var i = NativeMethods.hw_http_open();

			return;
		}

		private static void ResponseComplete(IntPtr user_data)
		{
		}


		private static void GetRoot(ref HttpRequest request, IntPtr response, IntPtr user_data)
		{
			NativeMethods.hw_set_response_status_code(response, ref statusCode); //ref statusCode);

			NativeMethods.hw_set_response_header(response, ref contenttypename, ref contenttypevalue);

			NativeMethods.hw_set_body(response, ref body);

			if (request.keep_alive > 0)
			{
				NativeMethods.hw_set_response_header(response, ref keepalivename, ref keepalivevalue);
			}
			else
			{
				NativeMethods.hw_set_http_version(response, 1, 0);
			}

			NativeMethods.hw_http_response_send(response, user_data, _responseRoot);
		}
	}
}