﻿using HaywireNet.Bindings.Extensions;
using HaywireNet.Bindings.Unsafe;
using HaywireNet.Bindings.Unsafe.Structs;
using System;
using System.Runtime.InteropServices;

namespace HelloWorld
{
	/// <summary>
	/// A simple hello world program based on the hello world sample in github.com/haywire/haywire
	/// </summary>
	public unsafe class Unsafe2Program
	{
		private const string RootRoute = "/";
		static readonly byte[] _rootRoute = RootRoute.ToAsciiNullArray();

		private const string PingRoute = "/ping";
		static readonly byte[] _pingRoute = PingRoute.ToAsciiNullArray();

		private const string DefaultAddress = "192.168.1.112";
		static readonly byte[] _defaultAddress = DefaultAddress.ToAsciiNullArray();

		private const string HttpOk = "200 OK";
		static readonly byte[] _httpOk = HttpOk.ToAsciiArray();

		private const string ContentTypeName = "Content-Type";
		static readonly byte[] _contentTypeName = ContentTypeName.ToAsciiArray();

		private const string ContentTypeValue = "text/html";
		static readonly byte[] _contentTypeValue = ContentTypeValue.ToAsciiArray();

		private const string KeepAliveName = "Connection";
		static readonly byte[] _keepAliveName = KeepAliveName.ToAsciiArray();

		private const string KeepAliveValue = "Keep-Alive";
		static readonly byte[] _keepAliveValue = KeepAliveValue.ToAsciiArray();

		private const string Body = "Hello World";
		static readonly byte[] _body = Body.ToAsciiArray();

		private const string UserData = "user_data";
		static readonly byte[] _userData = UserData.ToAsciiArray();

		private const uint DefaultPort = 8800;

		private const string Parser = "http_parser";
		static readonly byte[] _parser = Parser.ToAsciiNullArray();

		/*
		static Program()
		{
			HttpOk.SetStringASCII(out _httpOk);
		}
		*/


		
		public unsafe static void Main(string[] args)
		{
			try
			{
				fixed (byte* rootRoute = _rootRoute)
				fixed (byte* pingRoute = _pingRoute)
				fixed (byte* defaultAddress = _defaultAddress)
				fixed (byte* parser = _parser)
				{
					Console.WriteLine("2nd unsafe version");

					//IntPtr configPtr = Marshal.AllocHGlobal(sizeof(configuration));

					configuration config;

					config.http_listen_address = defaultAddress;
					config.http_listen_port = DefaultPort;
					config.thread_count = (uint)0;
					config.parser = parser;
					config.max_request_size = 1048576;
					config.tcp_nodelay = false;
					config.listen_backlog = 0;

					//Marshal.StructureToPtr<configuration>(config, configPtr, true);

					var program = new Unsafe2Program();

					Functions.hw_init_with_config(&config);

					Functions.hw_http_add_route(rootRoute, program.GetRoot, null);

					//Functions.hw_http_add_route(pingRoute, GetPing, (void*)0);

					var i = Functions.hw_http_open();
				}

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

		private void GetRoot(HttpRequest* request, void* response, void* user_data)
		{
			fixed(byte* httpOk = _httpOk)
			fixed(byte* contentTypeName = _contentTypeName)
			fixed(byte* contentTypeValue = _contentTypeValue)
			fixed(byte* body1 = _body)
			fixed(byte* keepAliveName = _keepAliveName)
			fixed(byte* keepAliveValue = _keepAliveValue)
			fixed(byte* userData = _userData)
			{
				HaywireString statusCode = new HaywireString(httpOk, HttpOk.Length);
				HaywireString contenttypename = new HaywireString(contentTypeName, ContentTypeName.Length);
				HaywireString contenttypevalue = new HaywireString(contentTypeValue, ContentTypeValue.Length);
				HaywireString body = new HaywireString(body1, Body.Length);
				HaywireString keepalivename = new HaywireString(keepAliveName, KeepAliveName.Length);
				HaywireString keepalivevalue = new HaywireString(keepAliveValue, KeepAliveValue.Length);
				//HaywireString routeMatchedName;
				//HaywireString routeMatchedValue;

				Functions.hw_set_response_status_code(response, &statusCode);

				Functions.hw_set_response_header(response, &contenttypename, &contenttypevalue);

				Functions.hw_set_body(response, &body);

				if (request->keep_alive > 0)
				{
					Functions.hw_set_response_header(response, &keepalivename, &keepalivevalue);
				}
				else
				{
					Functions.hw_set_http_version(response, 1, 0);
				}

				Functions.hw_http_response_send(response, userData, ResponseComplete);
			}
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
		/*
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

			Functions.hw_http_response_send(response, null, ResponseComplete);

			Console.WriteLine("line: " + line++);
		}
		*/
	}
}