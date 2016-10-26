using HaywireNet.Bindings.Extensions;
using HaywireNet.Bindings.Unsafe;
using HaywireNet.Bindings.Unsafe.Structs;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HelloWorld
{
	public unsafe class Program
	{
		const string RootRoute = "/";
		const string PingRoute = "/ping";
		const string DefaultAddress = "127.0.0.1";
		const uint DefaultPort = 8800;

		public static void Main(string[] args)
		{
			try
			{
				configuration config;

				config.http_listen_address = DefaultAddress.ToAsciiNullTerm();
				config.http_listen_port = DefaultPort;
				config.thread_count = 0;
				config.parser = "http_parser".ToAsciiNullTerm();
				config.max_request_size = 1048576;
				config.tcp_nodelay = true;
				config.listen_backlog = 0;

				Console.WriteLine(Directory.GetCurrentDirectory());

				//IntPtr unmanagedAddr = Marshal.AllocHGlobal(Marshal.SizeOf(config));

				//Marshal.StructureToPtr(config, unmanagedAddr, true);

				//void* ptr = null;

				Functions.hw_init_with_config(&config); //((configuration*)unmanagedAddr);

				Functions.hw_http_add_route(RootRoute.ToUtf8NullTerm(), GetRoot, null);

				Functions.hw_http_add_route(PingRoute.ToAsciiNullTerm(), GetPing, null);

				Functions.hw_http_open();

				return;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void ResponseComplete(void* user_data)
		{

		}

		static void GetRoot(HttpRequest* request, void* response, void* user_data)
		{
			HaywireString statusCode;
			HaywireString contentTypeName;
			HaywireString contentTypeValue;
			HaywireString body;
			HaywireString keepAliveName;
			HaywireString keepAliveValue;
			HaywireString routeMatchedName;
			HaywireString routeMatchedValue;

			"200 OK".SetStringASCII(out statusCode);

			Functions.hw_set_response_status_code(response, &statusCode);

			"Content-Type".SetStringASCII(out contentTypeName);

			"text/html".SetStringASCII(out contentTypeValue);

			Functions.hw_set_response_header(response, &contentTypeName, &contentTypeValue);

			"Hello World From C#".SetStringASCII(out body);
			Functions.hw_set_body(response, &body);

			if (request->keep_alive > 0)
			{
				"Connection".SetStringASCII(out keepAliveName);
				"Keep-Alive".SetStringASCII(out keepAliveValue);
				Functions.hw_set_response_header(response, &keepAliveName, &keepAliveValue);
			}
			else
			{
				Functions.hw_set_http_version(response, 1, 0);
			}

			Functions.hw_http_response_send(response, user_data, ResponseComplete);
		}

		static void GetPing(HttpRequest* request, void* response, void* user_data)
		{
			HaywireString statusCode;
			HaywireString contentTypeName;
			HaywireString contentTypeValue;
			HaywireString body;
			HaywireString keepAliveName;
			HaywireString keepAliveValue;
			HaywireString routeMatchedName;
			HaywireString routeMatchedValue;

			Functions.hw_print_request_headers(request);

			//Functions.hw_print_body(request)

			"200 OK".SetStringASCII(out statusCode);

			Functions.hw_set_response_status_code(response, &statusCode);

			"Content-Type".SetStringASCII(out contentTypeName);

			"text/html".SetStringASCII(out contentTypeValue);

			Functions.hw_set_response_header(response, &contentTypeName, &contentTypeValue);

			body.value = request->body->value;
			body.length = request->body->length;

			Functions.hw_set_body(response, &body);

			if (request->keep_alive > 0)
			{
				"Connection".SetStringASCII(out keepAliveName);
				"Keep-Alive".SetStringASCII(out keepAliveValue);
				Functions.hw_set_response_header(response, &keepAliveName, &keepAliveValue);
			}
			else
			{
				Functions.hw_set_http_version(response, 1, 0);
			}

			Functions.hw_http_response_send(response, user_data, ResponseComplete);
		}
	}
}
