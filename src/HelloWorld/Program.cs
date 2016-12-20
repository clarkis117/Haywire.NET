using System;
using System.IO;
using HaywireNet.Bindings;
using Newtonsoft.Json;
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
		private const string HttpOk = "200 OK";
		private const string ContentTypeName = "Content-Type";
		private const string ContentTypeValue = "text/html";
		private const string KeepAliveName = "Connection";
		private const string KeepAliveValue = "Keep-Alive";
		private const string Body = "Hello World";
		private const string ConfigFile = "./config.json";

		private static HaywireString statusCodeHttpOk = new HaywireString(HttpOk);
		private static HaywireString contentTypeName = new HaywireString(ContentTypeName);
		private static HaywireString contentTypeValue = new HaywireString(ContentTypeValue);
		private static HaywireString helloWorldBody = new HaywireString(Body);
		private static HaywireString keepAliveName = new HaywireString(KeepAliveName);
		private static HaywireString keepAliveValue = new HaywireString(KeepAliveValue);

		private static readonly http_request_callback _rootCb = GetRoot;
		private static readonly http_request_callback _pingCb = GetPing;

		private static readonly http_response_complete_callback _responseCb = ResponseComplete;

		public static void Main(string[] args)
		{
			Console.WriteLine("Haywire.NET Hello World");

			Configuration config;

			if(File.Exists(ConfigFile))
			{
				Console.WriteLine("Reading config File");
				config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConfigFile));
			}
			else
			{
				Console.WriteLine("No config file present writing config and loading defaults");

				config = new Configuration("localhost");

				using (FileStream fs = File.Open(@"./config.json", FileMode.CreateNew))
				using (StreamWriter sw = new StreamWriter(fs))
				using (JsonWriter jw = new JsonTextWriter(sw))
				{
					jw.Formatting = Formatting.Indented;

					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(jw, config);
				}
			}

			NativeMethods.hw_init_with_config(ref config);

			NativeMethods.hw_http_add_route(RootRoute, _rootCb, IntPtr.Zero);
			NativeMethods.hw_http_add_route(PingRoute, _pingCb, IntPtr.Zero);

			NativeMethods.hw_http_open();

			return;
		}

		private static void ResponseComplete(IntPtr user_data)
		{
		}

		private static void GetRoot(ref HttpRequest request, IntPtr response, IntPtr user_data)
		{
			NativeMethods.hw_set_response_status_code(response, ref statusCodeHttpOk);

			NativeMethods.hw_set_response_header(response, ref contentTypeName, ref contentTypeValue);

			NativeMethods.hw_set_body(response, ref helloWorldBody);

			if (request.keep_alive > 0)
			{
				NativeMethods.hw_set_response_header(response, ref keepAliveName, ref keepAliveValue);
			}
			else
			{
				NativeMethods.hw_set_http_version(response, 1, 0);
			}

			NativeMethods.hw_http_response_send(response, user_data, _responseCb);
		}

		private static void GetPing(ref HttpRequest request, IntPtr response, IntPtr user_data)
		{
			NativeMethods.hw_print_request_headers(ref request);
			NativeMethods.hw_print_body(ref request);

			NativeMethods.hw_set_response_status_code(response, ref statusCodeHttpOk);

			NativeMethods.hw_set_response_header(response, ref contentTypeName, ref contentTypeValue);

			//copy body from request and set it
			HaywireString pingBody = request.Body;
			NativeMethods.hw_set_body(response, ref pingBody);

			if (request.keep_alive > 0)
			{
				NativeMethods.hw_set_response_header(response, ref keepAliveName, ref keepAliveValue);
			}
			else
			{
				NativeMethods.hw_set_http_version(response, 1, 0);
			}

			NativeMethods.hw_http_response_send(response, user_data, _responseCb);
		}
	}
}