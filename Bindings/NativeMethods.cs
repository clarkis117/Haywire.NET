using HaywireNet.Bindings.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings
{
	//todo figure out library name handling
	//todo hw_http_response
	/// <summary>
	/// Bindings for the functions in haywire.h
	/// </summary>
	///
	public class Haywire
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_response_complete_callback(IntPtr user_data);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_request_callback(ref HttpRequest request, IntPtr response, IntPtr user_data);


		public class NativeMethods
		{
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern int hw_init_from_config([MarshalAs(UnmanagedType.LPStr)]string configuration_filename);

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern int hw_init_with_config(ref configuration config);

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern int hw_http_open();

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_http_add_route([MarshalAs(UnmanagedType.LPTStr)] string route, http_request_callback callback, IntPtr user_data);

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern HaywireString hw_get_header(ref HttpRequest request, ref HaywireString key);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_free_http_response(IntPtr response);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_set_http_version(IntPtr response, ushort major, ushort minor);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_set_response_status_code(IntPtr response, ref HaywireString status_code);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_set_response_header(IntPtr response, ref HaywireString name, ref HaywireString value);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_set_body(IntPtr response, ref HaywireString body);

			//hw_http_response* response
			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_http_response_send(IntPtr response, IntPtr user_data, http_response_complete_callback callback);

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_print_request_headers(ref HttpRequest request);

			[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
			public static extern void hw_print_body(ref HttpRequest request);
		}
	}
}
