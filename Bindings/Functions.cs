using HaywireNet.Bindings.Unsafe.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Unsafe
{
	//todo figure out library name handling
	//todo hw_http_response
	public unsafe static class Functions
	{
		public const string LibraryName = "libhaywire_shared.so"; //"./bin/Debug/netcoreapp1.0/libhaywire_shared.so";

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_request_callback(HttpRequest* request, void* response, void* user_data);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_response_complete_callback(void* user_data);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int hw_init_from_config(byte* configuration_filename);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int hw_init_with_config(void* config);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int hw_http_open();

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_http_add_route(byte* route, http_request_callback callback, void* user_data);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern HaywireString* hw_get_header(HttpRequest* request, HaywireString* key);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_free_http_response(void* response);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_http_version(void* response, ushort major, ushort minor);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_response_status_code(void* response, HaywireString* status_code);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_response_header(void* response, HaywireString* name, HaywireString* value);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_body(void* response, HaywireString* body);

		//hw_http_response* response
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_http_response_send(void* response, void* user_data, http_response_complete_callback callback);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_print_request_headers(HttpRequest* request);
	}
}
