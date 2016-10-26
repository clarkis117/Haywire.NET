﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Safe
{
	/// <summary>
	/// Goal of these methods is to makes a type safe as possible binds to haywire
	/// </summary>
	public unsafe class Functions
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_request_callback(HttpRequest* request, IntPtr response, IntPtr user_data);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void http_response_complete_callback(IntPtr user_data);

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int hw_init_from_config([MarshalAs(UnmanagedType.LPStr)]string configuration_filename);

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int hw_init_with_config(IntPtr config);

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int hw_http_open();

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_http_add_route([MarshalAs(UnmanagedType.LPStr)]string route, http_request_callback callback, IntPtr user_data);

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern HaywireString* hw_get_header(HttpRequest* request, HaywireString* key);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_free_http_response(IntPtr response);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_http_version(IntPtr response, ushort major, ushort minor);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_response_status_code(IntPtr response, HaywireString* status_code);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_response_header(IntPtr response, HaywireString* name, HaywireString* value);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_set_body(IntPtr response, HaywireString* body);

		//hw_http_response* response
		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_http_response_send(IntPtr response, IntPtr user_data, http_response_complete_callback callback);

		[DllImport(Constants.LibraryName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void hw_print_request_headers(HttpRequest* request);
	}
}