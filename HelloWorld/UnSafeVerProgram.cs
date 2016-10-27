using HaywireNet.Bindings.Extensions;
using HaywireNet.Bindings.Unsafe;
using HaywireNet.Bindings.Unsafe.Structs;
using System;

namespace HelloWorld
{
	/// <summary>
	/// A simple hello world program based on the hello world sample in github.com/haywire/haywire
	/// </summary>
	public unsafe class unsafeProgram
	{
		private const string RootRoute = "/";
		static readonly byte* _rootRoute = RootRoute.ToAsciiNullTerm();

		private const string PingRoute = "/ping";
		static readonly byte* _pingRoute = PingRoute.ToAsciiNullTerm();

		private const string DefaultAddress = "192.168.1.112";
		static readonly byte* _defaultAddress = DefaultAddress.ToAsciiNullTerm();

		private const string HttpOk = "200 OK";
		static readonly byte* _httpOk = HttpOk.ToAsciiString();

		private const string ContentTypeName = "Content-Type";
		static readonly byte* _contentTypeName = ContentTypeName.ToAsciiString();

		private const string ContentTypeValue = "text/html";
		static readonly byte* _contentTypeValue = ContentTypeValue.ToAsciiString();

		private const string KeepAliveName = "Connection";
		static readonly byte* _keepAliveName = KeepAliveName.ToAsciiString();

		private const string KeepAliveValue = "Keep-Alive";
		static readonly byte* _keepAliveValue = KeepAliveValue.ToAsciiString();

		private const string Body = "Hello World";
		static readonly byte* _body = Body.ToAsciiString();

		private const string UserData = "user_data";
		static readonly byte* _userData = UserData.ToAsciiString();

		private const uint DefaultPort = 8800;

		/*
		static Program()
		{
			HttpOk.SetStringASCII(out _httpOk);
		}
		*/

		public unsafe static void unsafeMain(string[] args)
		{
			try
			{
				Console.WriteLine("Unsafe Version");

				configuration config;

				config.http_listen_address = DefaultAddress.ToAsciiNullTerm();
				config.http_listen_port = DefaultPort;
				config.thread_count = (uint)0;
				config.parser = "http_parser".ToAsciiNullTerm();
				config.max_request_size = 1048576;
				config.tcp_nodelay = false;
				config.listen_backlog = uint.MaxValue / 2;

				Functions.hw_init_with_config(&config);

				Functions.hw_http_add_route(RootRoute.ToAsciiNullTerm(), GetRoot, (void*)0);

				Functions.hw_http_add_route(PingRoute.ToAsciiNullTerm(), GetPing, (void*)0);

				Functions.hw_http_open();

				return;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void ResponseComplete(void* user_data)
		{
		}

		private static void GetRoot(HttpRequest* request, void* response, void* user_data)
		{
			HaywireString statusCode = new HaywireString(_httpOk, HttpOk.Length);
			HaywireString contentTypeName = new HaywireString(_contentTypeName, ContentTypeName.Length);
			HaywireString contentTypeValue = new HaywireString(_contentTypeValue, ContentTypeValue.Length);
			HaywireString body = new HaywireString(_body, Body.Length);
			HaywireString keepAliveName = new HaywireString(_keepAliveName, KeepAliveName.Length);
			HaywireString keepAliveValue = new HaywireString(_keepAliveValue, KeepAliveValue.Length);
			//HaywireString routeMatchedName;
			//HaywireString routeMatchedValue;

			Functions.hw_set_response_status_code(response, &statusCode);

			Functions.hw_set_response_header(response, &contentTypeName, &contentTypeValue);

			Functions.hw_set_body(response, &body);

			if (request->keep_alive > 0)
			{
				Functions.hw_set_response_header(response, &keepAliveName, &keepAliveValue);
			}
			else
			{
				Functions.hw_set_http_version(response, 1, 0);
			}

			//var userData = new IntPtr(null); //"user_data".ToAsciiNullTerm()

			Functions.hw_http_response_send(response, _userData, ResponseComplete);
		}

		/*
		private static void GetRoot(ref HttpRequest request, IntPtr response, IntPtr user_data)
		{
			HaywireString statusCode;
			HaywireString contentTypeName;
			HaywireString contentTypeValue;
			HaywireString body;
			HaywireString keepAliveName;
			HaywireString keepAliveValue;
			//HaywireString routeMatchedName;
			//HaywireString routeMatchedValue;

			"200 OK".SetStringASCII(out statusCode);

			Functions.hw_set_response_status_code(response, ref statusCode);

			"Content-Type".SetStringASCII(out contentTypeName);

			"text/html".SetStringASCII(out contentTypeValue);

			Functions.hw_set_response_header(response, ref contentTypeName, ref contentTypeValue);

			"Hello World From C#".SetStringASCII(out body);

			Functions.hw_set_body(response, ref body);

			if (request.keep_alive > 0)
			{
				"Connection".SetStringASCII(out keepAliveName);
				"Keep-Alive".SetStringASCII(out keepAliveValue);
				Functions.hw_set_response_header(response, ref keepAliveName, ref keepAliveValue);
			}
			else
			{
				Functions.hw_set_http_version(response, 1, 0);
			}

			var userData = new IntPtr(null); //"user_data".ToAsciiNullTerm()

			Functions.hw_http_response_send(response, null, ResponseComplete);
		}
		*/

		private static void GetPing(HttpRequest* request, void* response, void* user_data)
		{
			HaywireString statusCode;
			HaywireString contentTypeName;
			HaywireString contentTypeValue;
			HaywireString body;
			HaywireString keepAliveName;
			HaywireString keepAliveValue;
			//HaywireString routeMatchedName;
			//HaywireString routeMatchedValue;

			Functions.hw_print_request_headers(request);

			int line = 0;
			//Functions.hw_print_body(request)

			Console.WriteLine("line: " + line++);

			"200 OK".SetStringASCII(out statusCode);

			Console.WriteLine("line: " + line++);

			Functions.hw_set_response_status_code(response, &statusCode);

			Console.WriteLine("line: " + line++);

			"Content-Type".SetStringASCII(out contentTypeName);

			"text/html".SetStringASCII(out contentTypeValue);

			Console.WriteLine("line: " + line++);

			Functions.hw_set_response_header(response, &contentTypeName, &contentTypeValue);

			Console.WriteLine("line: " + line++);

			//var requestBody = Marshal.PtrToStructure<HaywireString>(request.body);

			//body.value = requestBody.value;
			//body.length = requestBody.length;

			"Hello World".SetStringASCII(out body);

			Console.WriteLine("line: " + line++);

			Functions.hw_set_body(response, &body);

			Console.WriteLine("line: " + line++);

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

			Console.WriteLine("line: " + line++);

			//var userData = new IntPtr(null); //"user_data".ToAsciiNullTerm()

			Functions.hw_http_response_send(response, _userData, ResponseComplete);

			Console.WriteLine("line: " + line++);
		}
	}
}