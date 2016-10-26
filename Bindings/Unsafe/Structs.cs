using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Unsafe.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct HaywireString
	{
		public byte* value;
		public uint length;
	}

	/// <summary>
	/// Set String methods just like haywire's
	/// </summary>
	public unsafe static class StringMethods
	{
		public static byte* GetASCII(string clrString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			return bytes;
		}

		public static void SetStringASCII(out HaywireString hwString, string clrString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed(char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}

		public static void SetStringUTF8(out HaywireString hwString, string clrString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.UTF8.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Configuration
	{
		public byte* http_listen_address;
		public uint http_listen_port;
		public uint thread_count;
		public byte* parser;
		public bool tcp_nodelay;
		public uint listen_backlog;
		public uint max_request_size;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct HttpRequest
	{
		public ushort http_major;
		public ushort http_minor;
		public byte method;
		public int keep_alive;
		public HaywireString* url;
		public void* headers;
		public HaywireString* body;
		public uint body_length;
		public enum state { OK = 0, SIZE_EXCEEDED, BAD_REQUEST, INTERNAL_ERROR };
	}
}
