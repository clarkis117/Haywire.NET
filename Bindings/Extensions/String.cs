using HaywireNet.Bindings.Unsafe.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Extensions
{
	public unsafe static class String
	{
		public static byte* ToAsciiNullTerm(this string clrString)
		{
			var nullTermString = clrString + "\0";
			var length = nullTermString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = nullTermString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			return bytes;
		}

		public static byte* ToUtf8NullTerm(this string clrString)
		{
			var nullTermString = clrString + "\0";
			var length = nullTermString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = nullTermString)
			{
				System.Text.Encoding.UTF8.GetBytes(chars, length, bytes, length);
			}

			return bytes;
		}

		public static void SetStringASCII(this string clrString, out HaywireString hwString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}

		public static void SetStringUTF8(this string clrString, out HaywireString hwString)
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
}
